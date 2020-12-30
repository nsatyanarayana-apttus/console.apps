using MongoDb.Tools;
using System;
using System.IO;
using System.Reflection;

namespace MongoDb
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            string sql = "Select Id,Name,Apttus_Config2__ProductId__c, Apttus_Config2__OptionGroupId__r.Name, Apttus_Config2__OptionGroupId__r.Apttus_Config2__Label__c, Apttus_Config2__OptionGroupId__r.Apttus_Config2__Description__c, Apttus_Config2__OptionGroupId__c, Apttus_Config2__ParentOptionGroupId__c, Apttus_Config2__RootOptionGroupId__c, Apttus_Config2__RootSequence__c, Apttus_Config2__IsHidden__c, Apttus_Config2__IsLeaf__c, Apttus_Config2__MinOptions__c, Apttus_Config2__MaxOptions__c, Apttus_Config2__MinTotalQuantity__c, Apttus_Config2__MaxTotalQuantity__c, Apttus_Config2__MinTotalQuantityExpressionId__r.Apttus_Config2__Expression__c, Apttus_Config2__MaxTotalQuantityExpressionId__r.Apttus_Config2__Expression__c, Apttus_Config2__ModifiableType__c, Apttus_Config2__Sequence__c, Apttus_Config2__IsPicklist__c, Apttus_Config2__ContentType__c,Apttus_Config2__ProductAttributeGroupMemberId__r.Apttus_Config2__AttributeGroupId__c from Apttus_Config2__ProductOptionGroup__c where Apttus_Config2__ProductId__c in ('01t2D000003tTUkQAM') Order By Apttus_Config2__ParentOptionGroupId__c";

            //SqlParser sqlParser = new SqlParser();

            //Console.WriteLine(sqlParser.ParseSql(sql));
            JsonParser jp = new JsonParser();
            jp.ReadJson(null);

           Console.ReadLine();
        }
    }
}
