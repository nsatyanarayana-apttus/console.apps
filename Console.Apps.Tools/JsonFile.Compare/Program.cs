using Apttus.Lightsaber.Pricing.Common.Entities;
using ConsoleApp1.Service;
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
            string ls_responce = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\ls_response.json");
            string sfdc_response = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\sfdc_response.json");

            

            ResponseParseService rpservice = new ResponseParseService();

            List<LineItem>  sfdc_lineitems = rpservice.GetSFDCLineItems(sfdc_response);
            List<LineItem>  ls_lineitems = rpservice.GetLsLineItems(ls_responce);

            List<LineItem> lineitem47 = ls_lineitems.Where(x => x.PrimaryLineNumber >=1 && x.PrimaryLineNumber <= 47).ToList();
            ls_lineitems = lineitem47;

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

            DisplayService.Display(ls_lineitems, lookup);
            Console.WriteLine("\n");
            //Console.WriteLine("Number of non matching: " + notmatching.Count);

            Console.Read();
        }


        

    }
}
