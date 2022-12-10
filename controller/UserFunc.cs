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
                User user = new User(name, phone, password);
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
        public static User findUser(String phone, List<User> usersList)
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
    }
}