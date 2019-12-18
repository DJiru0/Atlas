using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Atlas.Models.Service
{
    public class LoggingService
    {//Logging Service

        public void WriteLog(string step)

        {

            string sLoggingServiceURL = "https://lcghackathoncosmosdbtest.azurewebsites.net/api/HttpTrigger1?code=JDUSlYH62KtX7nb0KkuHgsLNT19atf142CUMduvOrIhsFg/dz7U8DQ==";

            //Build the query

            var builder = new UriBuilder(sLoggingServiceURL);

            //builder.Port = -1;

            var query = HttpUtility.ParseQueryString(builder.Query);

            query["name"] = "ATlas Application";
            query["TimeStamp"] = DateTime.Now.ToString();

            query["task"] = step;


            builder.Query = query.ToString();

            string sURL = builder.ToString();


            //Call the Azure Function Logging service

            using (HttpClient client = new HttpClient())

            {

                Task<HttpResponseMessage> response = client.GetAsync(sURL);



                HttpResponseMessage msg = response.Result;



                using (HttpContent respContent = msg.Content)

                {

                    // ... Read the response as a string.

                    string tr = respContent.ReadAsStringAsync().Result;

                }

            }


        }
    }
}