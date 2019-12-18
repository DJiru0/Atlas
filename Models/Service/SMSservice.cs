using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Atlas.Models.Service
{
    public class SMSservice
    {
        //SMS Service

        public void CallSMSService()

        {



            string testSMSMessage = "https://testazfunctionapp20191213025059.azurewebsites.net/api/SMSService?code=oa5if8P4gQZ2HPDdszUJTPE22nfsAaJ49So/id6pgYEy3DmBdvrATg==";



            using (HttpClient client = new HttpClient())

            {

                Task<HttpResponseMessage> response = client.GetAsync(testSMSMessage);



                HttpResponseMessage msg = response.Result;



                using (HttpContent respContent = msg.Content)

                {

                    // ... Read the response as a string.

                    string tr = respContent.ReadAsStringAsync().Result;



                    //string teststr = @"{'fullname':'ravi','location':'ether'}";

                    // ... deserialize the response, we know it has a 'result' field

                    //TestClass azureResponse = JsonConvert.DeserializeObject<TestClass>(teststr);



                    //lbltest.Text = azureResponse.fullname + azureResponse.location;

                }

            }



        }
    }
}