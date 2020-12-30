using MongoDB.Driver;
using System;

namespace MongoDb.Tools
{
    public class MongoDbDemo
    {

        public void ConnectToDb()
        {
            string cs = "mongodb://root:Apttu#cms#engg#123@ussc-cms-eng-mdb-dev-01.apttuscloud.io:27017,ussc-cms-eng-mdb-dev-03.apttuscloud.io:27017,ussc-cms-eng-mdb-dev-02.apttuscloud.io:27017/?authSource=admin&replicaSet=replicaset&readPreference=primary&appname=MongoDB Compass&ssl=false";
            //MongoClient dbClient = new MongoClient("mongodb://root:Apttu%23cms%23engg%23123@ussc-cms-eng-mdb-dev-01.apttuscloud.io:27017,ussc-cms-eng-mdb-dev-03.apttuscloud.io:27017,ussc-cms-eng-mdb-dev-02.apttuscloud.io:27017/?authSource=admin&replicaSet=replicaset&readPreference=primary&appname=MongoDB%20Compass&ssl=false");
            MongoClient dbClient = new MongoClient(cs);

            var dbList = dbClient.ListDatabases().ToList();

            Console.WriteLine("The list of databases on this server is: ");
            foreach (var db in dbList)
            {
                Console.WriteLine(db);
            }
        }
    }
}
