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

        public async Task<List<User>> getAllUsers()
        {
            var collection = getCollection<User>(userCollection);
            var list = await collection.FindAsync(_ => true);
            return list.ToList();
        }

        // find user in the database with a phone number
        public async Task<bool> checkUser(String phone)
        {
            var collection = getCollection<User>(userCollection);
            bool result = await collection.Find(user => user.Phone == phone).AnyAsync();
            return result;
        }

        // find user in the database with a phone number and return the user
        public async Task<User> getUser(String phone)
        {
            var collection = getCollection<User>(userCollection);
            var result = await collection.Find(user => user.Phone == phone).FirstOrDefaultAsync();
            return result;
        }

        // add user to a chat room
        public async Task addUserToChatRoom(User user, ChatRoom chatRoom)
        {
            var collection = getCollection<ChatRoom>(chatRoomCollection);
            var filter = Builders<ChatRoom>.Filter.Eq("Name", chatRoom.Name);
            var update = Builders<ChatRoom>.Update.Push("Users", user);
            await collection.UpdateOneAsync(filter, update);
        }

        // create a new chat room
        public async Task createChatRoom(ChatRoom chatRoom)
        {
            var collection = getCollection<ChatRoom>(chatRoomCollection);
            await collection.InsertOneAsync(chatRoom);
        }

        // add multiple users into a chat room
        public async Task addUsersToChatRoom(ChatRoom chatRoom, List<User> users)
        {
            var collection = getCollection<ChatRoom>(chatRoomCollection);
            var filter = Builders<ChatRoom>.Filter.Eq("Name", chatRoom.Name);
            var update = Builders<ChatRoom>.Update.PushEach("Users", users);
            await collection.UpdateOneAsync(filter, update);
        }

        public Task? createUser(User user)
        {
            // if the phone number is already in the database, return false
            if (checkUser(user.Phone).Result)
            {
                Console.WriteLine("Phone number is already in use");
                return null;
            }

            var collection = getCollection<User>(userCollection);
            return collection.InsertOneAsync(user);
        }
    }
}