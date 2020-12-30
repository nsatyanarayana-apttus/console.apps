using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Reflection;

namespace MongoDb.Tools
{
    public class JsonParser
    {
        SqlParser sqlParser = new SqlParser();

        public void ReadJson(string filename)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"JsonData\tracer.json");
            string[] lines = File.ReadAllLines(path);

            string output = string.Format(@"{0}\{1}.txt", Directory.GetCurrentDirectory(), "output");

            Console.WriteLine(output);


            using (StreamWriter oFile = new StreamWriter(output))
            {
                foreach (string line in lines)
                {
                    if (line.Contains("Query-->"))
                    {
                        oFile.WriteLine();
                        oFile.WriteLine(sqlParser.ParseSql(line.Split("Query-->")[1]));
                        oFile.WriteLine();
                        // Console.WriteLine();
                    }
                }
            }
        }

        private JObject GetJObject(string filename)
        {
            string FileName = string.Format(@"JsonData\{0}", filename);
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), FileName);
            JObject jobject = JObject.Parse(File.ReadAllText(path));

            return jobject;
        }
    }
}
