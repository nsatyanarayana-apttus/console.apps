using Apttus.Lightsaber.Pricing.Common.Entities;
using Apttus.Lightsaber.Pricing.Common.Formula;
using ConsoleApp1.Service;
using JsonFile.Compare.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {

            //ResultParser();

            SObjectService sos = new SObjectService();

            sos.DisplayObjectNamesByContains("pricerule");

            Console.WriteLine("------------------done---------------");
            Console.Read();
        }


        public static void ResultParser()
        {
            string ls_responce = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\ls_response.json");
            string sfdc_response = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\sfdc_response.json");



            ResponseParseService rpservice = new ResponseParseService();

            List<LineItem> sfdc_lineitems = rpservice.GetSFDCLineItems(sfdc_response);
            List<LineItem> ls_lineitems = rpservice.GetLsLineItems(ls_responce);
            //List<LineItem> sfdc_lineitems = rpservice.GetSFDCLineItems(sfdc_response).Where(x => x.PrimaryLineNumber >= 26 && x.PrimaryLineNumber <= 38).ToList();
            //List<LineItem> ls_lineitems = rpservice.GetLsLineItems(ls_responce).Where(x => x.PrimaryLineNumber >= 26 && x.PrimaryLineNumber <= 38).ToList();

            //List<LineItem> lineitem47 = ls_lineitems.Where(x => x.PrimaryLineNumber >=26 && x.PrimaryLineNumber <= 28).ToList();
            //ls_lineitems = lineitem47;

            //Console.WriteLine("Number of lines in sfdc cart : "+sfdc_lineitems.Count);
            //Console.WriteLine("Number of lines in ls   cart : "+ls_lineitems.Count);
            Console.WriteLine();

            IDictionary<string, LineItem> lookup = new Dictionary<string, LineItem>();
            sfdc_lineitems.ForEach(x => lookup.Add(x.PriceListItemId + "_" + x.PrimaryLineNumber, x));

            List<string> notexist = new List<string>();


            List<LineItem> notmatching = new List<LineItem>();
            List<LineItem> matching = new List<LineItem>();


            //int count = MatchService.MatchNetPrice(ls_lineitems, lookup, matching, notmatching, notexist);
            //DisplayService.DisplayNetPrice(notmatching, lookup);

            //count = MatchService.MatchListPrice(ls_lineitems, lookup, matching, notmatching, notexist);
            //DisplayService.DisplayListPrice(notmatching, lookup);

            //count = MatchService.MatchBasePrice(ls_lineitems, lookup, matching, notmatching, notexist);
            //DisplayService.DisplayBasePrice(notmatching, lookup);

            //DisplayService.Display(ls_lineitems, lookup);
            //sfdc_lineitems.ForEach(x => Console.Write("\'" + x.GroupAdjustmentPercent + "\',"));

            string str = JsonConvert.SerializeObject(sfdc_lineitems.FirstOrDefault());

            JObject respose = JObject.Parse(str);


            //foreach (KeyValuePair<string, JToken> property in respose)
            //{
            //    Console.WriteLine(property.Key + " - " + property.Value);
            //}

            foreach (JProperty property in respose.Properties())
            {
                if (property.Name != null && property.Name.ToLower().Contains("adjust"))
                {
                    //Console.WriteLine(property.Name);
                    Console.Write(property.Name + ",");
                }

            }

            //respose.Properties().ToList().ForEach(x => Console.WriteLine(x.Name));
            Console.WriteLine("\n");
            //Console.WriteLine("Number of non matching: " + notmatching.Count);
            Console.WriteLine("------------------done---------------------");
        }

    }
}
