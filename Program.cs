using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// import random number generator
using System.Security.Cryptography;
using _1651Assignment.controller;
using _1651Assignment.model;
using _1651Assignment.DataAccess;
using MongoDB.Driver;

namespace _1651Assignment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<User> usersList = new List<User>();
            // create a new user and add it to the list
            UserFunc.createUser("John Cena", "1234567890", "password", usersList);
            UserFunc.createUser("Joseph", "0987654321", "password", usersList);
            // create 5 more users with random phone number consists of 10 digits number
            for (int i = 0; i < 5; i++)
            {
                UserFunc.createUser("User " + i, MiscFuncs.randomStringDigit(10), "password", usersList);
            }
            // display all user in the list
            UserFunc.displayUsers(usersList);

            // user with phone number 1234567890 create a new chat room
            User? userToCreate = UserFunc.findUser("1234567890", usersList);
            ChatRoom chatRoom = new ChatRoom("Chat Room 1", userToCreate);
            // add 3 users to the chat room
            UserFunc.addUsersToChatRoom(userToCreate, chatRoom, usersList.GetRange(0, 3));
            // toString() method of ChatRoom class will display all users in the chat room
            // Console.WriteLine(chatRoom);
            
            // user with phone number 1234567890 send a message to the chat room
            userToCreate.sendMessage("Hello World!", chatRoom);
            usersList[1].sendMessage("Hello " + usersList[0].Name, chatRoom);
            // toString() method of ChatRoom class will display all messages in the chat room
            Console.WriteLine("chatRoom messages: ");
            chatRoom.displayMessages();

            Console.WriteLine("chatRoom users: ");
            chatRoom.showUsers();
            Console.WriteLine("Delete user: " + usersList[1]);
            // user with phone number 1234567890 remove user with phone number 0987654321 from the chat room and display all users in the chat room
            userToCreate.removeUser(chatRoom, usersList[1]);
            Console.WriteLine("chatRoom users: ");
            chatRoom.showUsers();
            Console.WriteLine("chatRoom admins: ");
            chatRoom.showAdmins();
            // first admin of the chat room remove the chat room
            // try
            // {
            //     userToCreate.deleteRoom(chatRoom);
            // }
            // catch (Exception e)
            // {
            //     Console.WriteLine(e.Message);
            //     Console.WriteLine(chatRoom);
            // }
            Console.WriteLine(chatRoom);

            try
            {
                Console.WriteLine("User " + usersList[0] + " send message to user " + usersList[1]);
                usersList[0].sendChatOneMessage("Hello " + usersList[1].Name, usersList[1]);
                Console.WriteLine("User " + usersList[1] + " send message to user " + usersList[0]);
                usersList[1].sendChatOneMessage("Hello " + usersList[0].Name, usersList[0]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            // display all chats of user[0]
            Console.WriteLine("User " + usersList[0] + " chats: ");
            UserFunc.displayUserChats(usersList[0]);

            // Console.WriteLine(DataAccess.getConnectionKey());
            // DataAccess.DataAccess.getConnectionKey();

            MainAsync().Wait();
        }

        static async Task MainAsync() {
            DataAccess.DataAccess db = new DataAccess.DataAccess();
            try {
                await db.createUser(new User() {Name = "John Cena", Phone = "123456789", Password = "password"});
                await db.createUser(new User() {Name = "Joseph", Phone = "098765432", Password = "password"});
                await db.createUser(new User() {Name = "User 1", Phone = MiscFuncs.randomStringDigit(10), Password = "password"});
                await db.createUser(new User() {Name = "User 2", Phone = MiscFuncs.randomStringDigit(10), Password = "password"});
                await db.createUser(new User() {Name = "User 3", Phone = MiscFuncs.randomStringDigit(10), Password = "password"});
                await db.createUser(new User() {Name = "User 4", Phone = MiscFuncs.randomStringDigit(10), Password = "password"});
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            // Console.WriteLine(db.findUser("123456789"));
            var users = await db.getAllUsers();
            foreach (var user in users)
            {
                Console.WriteLine(user);
            }
            try {
            // create a new chat room with John Cena as the admin
            ChatRoom chatRoom = new ChatRoom("Chat Room 1", await db.getUser("123456789"));
            await db.createChatRoom(chatRoom);
            // add 3 users to the chat room
            List<User> usersToAdd = new List<User>();
            usersToAdd.Add(await db.getUser("098765432"));
            usersToAdd.Add(await db.getUser("2118301106"));
            usersToAdd.Add(await db.getUser("0774808041"));
            await db.addUsersToChatRoom(chatRoom, usersToAdd);
            // await db.addUsersToChatRoom(chatRoom, new List<User>() {await db.getAllUsers().Result[1], await db.getAllUsers().Result[2], await db.getAllUsers().Result[3]});
            // toString() method of ChatRoom class will display all users in the chat room
            Console.WriteLine(chatRoom);
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }
    }
}