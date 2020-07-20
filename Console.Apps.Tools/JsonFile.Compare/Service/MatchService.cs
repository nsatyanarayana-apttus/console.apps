using Apttus.Lightsaber.Pricing.Common.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ConsoleApp1.Service
{
    public class MatchService
    {

        public static int MatchNetPrice(List<LineItem> ls_lineitems, IDictionary<string, LineItem> lookup, List<LineItem> matching, List<LineItem> notmatching, List<string> notexist)
        {
            int count = 0;
            if (ls_lineitems.Count > 0)
            {
                ls_lineitems.ForEach(x => {

                    string key = x.PriceListItemId + "_" + x.PrimaryLineNumber;

                    if (lookup.ContainsKey(key))
                    {
                        ++count;
                        LineItem sfdc = lookup[key];
                        //Console.WriteLine(sfdc.NetPrice +" : " + x.NetPrice);

                        if ((sfdc.NetPrice.GetValueOrDefault() - x.NetPrice.GetValueOrDefault()) != 0)
                        {
                            notmatching.Add(x);
                        }
                        else
                        {
                            matching.Add(x);
                        }
                    }
                    else
                    {
                        notexist.Add(key);

                    }

                });
            }

            return count;
        }

        public static int MatchListPrice(List<LineItem> ls_lineitems, IDictionary<string, LineItem> lookup, List<LineItem> matching, List<LineItem> notmatching, List<string> notexist)
        {
            int count = 0;
            if (ls_lineitems.Count > 0)
            {
                ls_lineitems.ForEach(x => {

                    string key = x.PriceListItemId + "_" + x.PrimaryLineNumber;

                    if (lookup.ContainsKey(key))
                    {
                        ++count;
                        LineItem sfdc = lookup[key];
                        //Console.WriteLine(sfdc.NetPrice +" : " + x.NetPrice);

                        if ((sfdc.ListPrice.GetValueOrDefault() - x.ListPrice.GetValueOrDefault()) != 0)
                        {
                            notmatching.Add(x);
                        }
                        else
                        {
                            matching.Add(x);
                        }
                    }
                    else
                    {
                        notexist.Add(key);

                    }

                });
            }

            return count;
        }

        public static int MatchBasePrice(List<LineItem> ls_lineitems, IDictionary<string, LineItem> lookup, List<LineItem> matching, List<LineItem> notmatching, List<string> notexist)
        {
            int count = 0;
            if (ls_lineitems.Count > 0)
            {
                ls_lineitems.ForEach(x => {

                    string key = x.PriceListItemId + "_" + x.PrimaryLineNumber;

                    if (lookup.ContainsKey(key))
                    {
                        ++count;
                        LineItem sfdc = lookup[key];
                        //Console.WriteLine(sfdc.NetPrice +" : " + x.NetPrice);

                        if ((sfdc.BasePrice.GetValueOrDefault() - x.BasePrice.GetValueOrDefault()) != 0)
                        {
                            notmatching.Add(x);
                        }
                        else
                        {
                            matching.Add(x);
                        }
                    }
                    else
                    {
                        notexist.Add(key);

                    }

                });
            }

            return count;
        }
    }
}
