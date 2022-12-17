using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1651Assignment.model;

namespace _1651Assignment.controller
{
    public class UserFunc
    {
        // create a new user and add it to the list
        public static void createUser(String name, String phone, String password, List<User> usersList)
        {
            // check if phone number is already in use
            if (findUser(phone, usersList) == null)
            {
                User user = new User();
                user.Name = name;
                user.Phone = phone;
                user.Password = password;
                user.Chats = new List<ObjectId>();
                usersList.Add(user);
            }
            else
            {
                Console.WriteLine("Phone number is already in use");
            }
            // User user = new User(name, phone, password);
            // usersList.Add(user);
        }
        
        // find a user by phone number
        public static User? findUser(String phone, List<User> usersList)
        {
            foreach (User user in usersList)
            {
                if (user.Phone.Equals(phone))
                {
                    return user;
                }
            }
            return null;
        }

        // display all users
        public static void displayUsers(List<User> usersList)
        {
            foreach (User user in usersList)
            {
                Console.WriteLine(user);
            }
        }

        // add multiple users to a chat room
        public static void addUsersToChatRoom(User user, ChatRoom chatRoom, List<User> usersList)
        {
            foreach (User u in usersList)
            {
                user.addUser(chatRoom, u);
            }
        }

        // display all chats of a user
        public static void displayUserChats(User user)
        {
            try
            {
                foreach (ObjectId chat in user.Chats)
                {
                    Console.WriteLine(chat);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new System.Exception("User has no chats");
            }
            
        }
    }
}