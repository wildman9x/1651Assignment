using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using dotenv.net;
using _1651Assignment.model;

namespace _1651Assignment.DataAccess
{
    public class DataAccess
    {
        public static string getEnv(string key)
        {
            // DotEnv.Load(options: new DotEnvOptions ( envFilePaths: new[] { "D:/Class/1651/1651Assignment/DataAccess/.env" } ));
            // var envVars = DotEnv.Read();
            // return envVars[key];
            DotNetEnv.Env.Load();
            return DotNetEnv.Env.GetString(key);
        }
        // get the connection string from the environment variable
        // private const var getEnv = DotEnv.Read();
        private static string connectionString = getEnv("MONGO_CONNECTION_STRING");
        private const string databaseName = "1651Assignment";
        private const string userCollection = "Users";
        // private const string chatMediatorCollection = "ChatMediator";
        private const string chatRoomCollection = "ChatRoom";
        private const string messageCollection = "Message";
        private const string chatOneCollection = "ChatOne";

        public static void getConnectionKey() {
            Console.WriteLine(connectionString);
        }

        private static IMongoCollection<T> getCollection<T>(string collectionName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            return database.GetCollection<T>(collectionName);
        }

        private async Task<List<T>> getCollectionList<T>(string collectionName)
        {
            var collection = getCollection<T>(collectionName);
            var list = await collection.Find(_ => true).ToListAsync();
            return list;
        }

        public static async Task<List<T>> getAllUsers<T>()
        {
            var collection = getCollection<T>(userCollection);
            var list = await collection.Find(_ => true).ToListAsync();
            return list;
        }

        public static Task createUser(User user)
        {
            var collection = getCollection<User>(userCollection);
            return collection.InsertOneAsync(user);
        }
    }
}