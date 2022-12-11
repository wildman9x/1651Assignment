using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// import random number generator
using System.Security.Cryptography;
using _1651Assignment.controller;
using _1651Assignment.model;

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
            Random rng = new Random();
            for (int i = 0; i < 5; i++)
            {
                String phone = "";
                for (int j = 0; j < 10; j++)
                {
                    phone += rng.Next(0, 10);
                }
                UserFunc.createUser("User" + i, phone, "password", usersList);
            }
            // display all user in the list
            foreach (User user in usersList)
            {
                Console.WriteLine(user);
            }

            // user with phone number 1234567890 create a new chat room
            User userToCreate = UserFunc.findUser("1234567890", usersList);
            ChatRoom chatRoom = new ChatRoom("Chat Room 1", userToCreate);
            // add 3 users to the chat room
            for (int i = 0; i < 3; i++)
            {
                userToCreate.addUser(chatRoom, usersList[i]);
            }
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
            // user with phone number 1234567890 remove user with phone number 0987654321 from the chat room and display all users in the chat room
            userToCreate.removeUser(chatRoom, usersList[1]);
            Console.WriteLine("chatRoom users: ");
            chatRoom.showUsers();
            chatRoom.showAdmins();
            // first admin of the chat room remove the chat room
            try
            {
                userToCreate.deleteRoom(chatRoom);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(chatRoom);
            }
            Console.WriteLine(chatRoom);
        }
    }
}