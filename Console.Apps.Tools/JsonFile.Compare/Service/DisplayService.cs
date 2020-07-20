using Apttus.Lightsaber.Pricing.Common.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ConsoleApp1.Service
{
    public class DisplayService
    {

        public static string space = ",";

        public static void DisplayNetPrice(List<LineItem> lineitems, IDictionary<string, LineItem> lookup)
        {
            if (lineitems.Count > 0)
            {
                Console.WriteLine(" Primary Line Number, PriceListItemId, LS NetPrice, SFDC NetPrice");
                lineitems.ForEach(x => {

                    string key = x.PriceListItemId + "_" + x.PrimaryLineNumber;
                    LineItem sfdc = lookup[key];
                    Console.WriteLine(x.PrimaryLineNumber + space + x.PriceListItemId + space + x.NetPrice + space + sfdc.NetPrice);
                });
            }
        }

        public static void DisplayListPrice(List<LineItem> lineitems, IDictionary<string, LineItem> lookup)
        {
            if (lineitems.Count > 0)
            {
                Console.WriteLine(" Primary Line Number, PriceListItemId, LS ListPrice, SFDC ListPrice");
                lineitems.ForEach(x => {

                    string key = x.PriceListItemId + "_" + x.PrimaryLineNumber;
                    LineItem sfdc = lookup[key];
                    Console.WriteLine(x.PrimaryLineNumber + space + x.PriceListItemId + space + x.ListPrice + space + sfdc.ListPrice);
                });
            }
        }

        public static void DisplayBasePrice(List<LineItem> lineitems, IDictionary<string, LineItem> lookup)
        {
            if (lineitems.Count > 0)
            {
                Console.WriteLine(" Primary Line Number, PriceListItemId, LS BasePrice, SFDC BasePrice");
                lineitems.ForEach(x => {

                    string key = x.PriceListItemId + "_" + x.PrimaryLineNumber;
                    LineItem sfdc = lookup[key];
                    Console.WriteLine(x.PrimaryLineNumber + space + x.PriceListItemId + space + x.BasePrice + space + sfdc.BasePrice);
                });
            }
        }


        public static void Display(List<LineItem> lineitems, IDictionary<string, LineItem> lookup)
        {
            if (lineitems.Count > 0)
            {
                var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\output.csv");
                using (StreamWriter file = new StreamWriter(path))
                {
                    //file.WriteLine(" Apttus_Config2__PrimaryLineNumber__c,id, Apttus_Config2__PriceListItemId__c,CurrencyIsoCode,Apttus_Config2__AdjustedPrice__c,Apttus_Config2__ChargeType__c,Apttus_Config2__DeltaPrice__c,Apttus_Config2__ExtendedPrice__c,Apttus_Config2__ExtendedQuantity__c,Apttus_Config2__Frequency__c,Apttus_Config2__NetPrice__c,Apttus_Config2__OptionId__c,Apttus_Config2__NetUnitPrice__c,Apttus_Config2__OptionPrice__c,Apttus_Config2__ProductOptionId__c,Apttus_Config2__Quantity__c,Apttus_Config2__SellingFrequency__c,Apttus_Config2__SellingTerm__c,Apttus_Config2__SellingUom__c,Apttus_Config2__BasePrice__c,Apttus_Config2__BasePriceOverride__c,Apttus_Config2__AdjustmentType__c,Apttus_Config2__AdjustmentAmount__c,Apttus_Config2__ListPrice__c,Apttus_Config2__BaseExtendedPrice__c,Apttus_Config2__NetAdjustmentPercent__c,Apttus_Config2__FlatOptionPrice__c,Apttus_Config2__GroupAdjustmentPercent__c,LineNumber,ProductId,LineType,PraentBundleNumber");
                    file.WriteLine("LineItemId, PrimaryLineNumber, PriceListItemId ,CurrencyIsoCode,AdjustedPrice,ChargeType,DeltaPrice,ExtendedPrice,ExtendedQuantity,Frequency,NetPrice,OptionId,NetUnitPrice,OptionPrice,ProductOptionId,Quantity,SellingFrequency,SellingTerm,SellingUom,BasePrice,BasePriceOverride,AdjustmentType,AdjustmentAmount,ListPrice,BaseExtendedPrice,NetAdjustmentPercent,FlatOptionPrice,GroupAdjustmentPercent,LineNumber,ProductId,LineType,PraentBundleNumber");
                    lineitems.ForEach(x => {

                        string key = x.PriceListItemId + "_" + x.PrimaryLineNumber;
                        LineItem sfdc = lookup[key];
                        file.WriteLine(x.Name + space + x.PrimaryLineNumber + space +    x.PriceListItemId  + space + x.CurrencyIsoCode    + space + x.AdjustedPrice + space + x.ChargeType + space + x.DeltaPrice + space + x.ExtendedPrice + space + x.ExtendedQuantity + space + x.Frequency + space + x.NetPrice + space + sfdc.OptionId + space + x.NetUnitPrice + space + x.OptionPrice + space + x.ProductOptionId + space + x.Quantity + space + x.SellingFrequency + space + x.SellingTerm + space + x.SellingUom + space + x.BasePrice + space + x.BasePriceOverride + space + x.AdjustmentType + space + x.AdjustmentAmount + space + x.ListPrice + space + x.BaseExtendedPrice + space + x.NetAdjustmentPercent + space + x.FlatOptionPrice + space + x.GroupAdjustmentPercent + space + x.LineNumber + space + x.ProductId + space + x.LineType + space + x.ParentBundleNumber);
                        file.WriteLine(sfdc.Name + space + sfdc.PrimaryLineNumber + space + sfdc.PriceListItemId  + space + sfdc.CurrencyIsoCode + space + sfdc.AdjustedPrice + space + sfdc.ChargeType + space + sfdc.DeltaPrice + space + sfdc.ExtendedPrice + space + sfdc.ExtendedQuantity + space + sfdc.Frequency + space + sfdc.NetPrice + space + sfdc.OptionId + space + sfdc.NetUnitPrice + space + sfdc.OptionPrice + space + sfdc.ProductOptionId + space + sfdc.Quantity + space + sfdc.SellingFrequency + space + sfdc.SellingTerm + space + sfdc.SellingUom + space + sfdc.BasePrice + space + sfdc.BasePriceOverride + space + sfdc.AdjustmentType + space + sfdc.AdjustmentAmount + space + sfdc.ListPrice + space + sfdc.BaseExtendedPrice + space + sfdc.NetAdjustmentPercent + space + sfdc.FlatOptionPrice + space + sfdc.GroupAdjustmentPercent + space + sfdc.LineNumber + space + sfdc.ProductId + space + sfdc.LineType + space + x.ParentBundleNumber);
                    });
                }

            }

        }

    }
}
