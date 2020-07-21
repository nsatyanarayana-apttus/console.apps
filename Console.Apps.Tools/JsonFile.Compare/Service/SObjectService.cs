using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace JsonFile.Compare.Service
{
    public class SObjectService
    {

        private  JObject sobjects;
        public SObjectService()
        {
            string sobjects_path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\sobjects.json");
            string sobjects_json = File.ReadAllText(sobjects_path);
            this.sobjects = JObject.Parse(sobjects_json);
        }

        public List<string> GetAllObjectNames()
        {
            List<string> soNames = new List<string>();
            if (sobjects!=null)
            {
                JArray jArray = (JArray)sobjects["sobjects"];

                jArray.ToList().ForEach(x => soNames.Add(x["name"].ToString().ToLower()));
            }
            return soNames;
        }

        public void DisplayObjectNamesByContains(string contains)
        {
            GetAllObjectNames().ForEach(x => { 
            
                if(x.Contains(contains))
                {
                    System.Console.WriteLine(x);
                }
            });
        }
    }
}
