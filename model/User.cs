using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1651Assignment.model
{
    public class User
    {
        public String Name { get; set; }
        public String Phone { get; set; }
        // private password
        private String Password { get; set; }
        public List<ChatRoom> chatRooms { get; set; }

        public User(String name, String phone, String password)
        {
            Name = name;
            Phone = phone;
            Password = password;
        }

        public bool checkPassword(String password)
        {
            return Password.Equals(password);
        }

        public void sendMessage(String message, chatMediator chatRoom)
        {
            chatRoom.addMessage(message, this);
        }

        public void getNotification(String message, chatMediator chatRoom)
        {
            // TODO
            Console.WriteLine("User: " + Name + " received notification: " + message + " from chat room: " + chatRoom.Name);
        }

        public void leaveRoom(ChatRoom chatRoom)
        {
            chatRoom.Users.Remove(this);
        }

        // create room and make this user admin of the room
        public void createRoom(String name)
        {
            ChatRoom chatRoom = new ChatRoom(name, this);
            chatRooms.Add(chatRoom);
            // chatRoom.addUser(this);
            chatRoom.Admins.Add(this);
        }

        // toString
        public override string ToString()
        {
            return "User{" +
                    "Name='" + Name + '\'' +
                    ", Phone='" + Phone + '\'' +
                    '}';
        }
    }
}