using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MongoDb.Tools
{
    public class SqlParser
    {
        private string SELECT = "SELECT";
        private string FROM = "FROM";
        private string WHERE = "WHERE";

        public string ParseSql(string sql)
        {
            StringBuilder sqlbuilder = new StringBuilder();

            string[] select_token = sql.ToUpper().Split(FROM, StringSplitOptions.RemoveEmptyEntries);

            if(select_token.Length == 2)
            {
                string[] fields = select_token[0].Split(SELECT)[1].Split(',');
                sqlbuilder.Append(SELECT);
                fields.ToList().ForEach(field => {

                    sqlbuilder.Append("\n\t");
                    sqlbuilder.Append(field.Trim());
                    sqlbuilder.Append(",");
                });
                sqlbuilder.Remove(sqlbuilder.Length - 1, 1);

                string[] from_token = select_token[1].Split(WHERE, StringSplitOptions.RemoveEmptyEntries);
                sqlbuilder.Append("\n");
                sqlbuilder.Append(FROM);
                sqlbuilder.Append("\n\t");
                sqlbuilder.Append(from_token[0].Trim());
                sqlbuilder.Append("\n");
                sqlbuilder.Append(WHERE);
                sqlbuilder.Append("\n\t");
                sqlbuilder.Append(from_token[1].Trim());
            }
            else
            {
                sqlbuilder.Append(sql);
            }
            return sqlbuilder.ToString();
        }

        private void method()
        {

            //string sql = "Select Id,Name,Apttus_Config2__ProductId__c, Apttus_Config2__OptionGroupId__r.Name, Apttus_Config2__OptionGroupId__r.Apttus_Config2__Label__c, Apttus_Config2__OptionGroupId__r.Apttus_Config2__Description__c, Apttus_Config2__OptionGroupId__c, Apttus_Config2__ParentOptionGroupId__c, Apttus_Config2__RootOptionGroupId__c, Apttus_Config2__RootSequence__c, Apttus_Config2__IsHidden__c, Apttus_Config2__IsLeaf__c, Apttus_Config2__MinOptions__c, Apttus_Config2__MaxOptions__c, Apttus_Config2__MinTotalQuantity__c, Apttus_Config2__MaxTotalQuantity__c, Apttus_Config2__MinTotalQuantityExpressionId__r.Apttus_Config2__Expression__c, Apttus_Config2__MaxTotalQuantityExpressionId__r.Apttus_Config2__Expression__c, Apttus_Config2__ModifiableType__c, Apttus_Config2__Sequence__c, Apttus_Config2__IsPicklist__c, Apttus_Config2__ContentType__c,Apttus_Config2__ProductAttributeGroupMemberId__r.Apttus_Config2__AttributeGroupId__c from Apttus_Config2__ProductOptionGroup__c where Apttus_Config2__ProductId__c in ('01t2D000003tTUkQAM') Order By Apttus_Config2__ParentOptionGroupId__c";
            //SqlParser sqlParser = new SqlParser();
            //Console.WriteLine(sqlParser.ParseSql(sql));
        }
    }
}
