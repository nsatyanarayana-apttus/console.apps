using Apttus.Lightsaber.Pricing.Common.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ConsoleApp1.Service
{
    public class ResponseParseService
    {
        
        public List<LineItem> GetLsLineItems(string ls_responce)
        {
            //string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\ls_response.json");
            string text = File.ReadAllText(ls_responce);

            JObject respose = JObject.Parse(text);

            JToken token = respose.SelectToken("CartResponse.Apttus_Config2__LineItems__r");

            return token.ToObject<List<LineItem>>();
        }

        public List<LineItem> GetSFDCLineItems(string sfdc_response)
        {
            //string sfdc_response = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\sfdc_response.json");
            string text = File.ReadAllText(sfdc_response);

            JObject respose = JObject.Parse(text);

            JToken token = respose.SelectToken("CartResponse");

            return token.ToObject<List<LineItem>>();
        }

      
    }
}
