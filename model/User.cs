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
            Console.WriteLine("User: " + Name + " \n received notification: " + message + "\n from chat room: " + chatRoom.Name + "\n");
        }

        public void leaveRoom(ChatRoom chatRoom)
        {
            chatRoom.removeUser(this);
            if (isAdmin(chatRoom))
            {
                chatRoom.removeAdmin(this);
            }
        }

        public Boolean isAdmin(ChatRoom chatRoom)
        {
            return chatRoom.containAdmin(this);
        }

        public void addAdmin(ChatRoom chatRoom, User user)
        {
            if (isAdmin(chatRoom))
            {
                chatRoom.addAdmin(user);
            }
        }

        public void removeAdmin(ChatRoom chatRoom, User user)
        {
            if (isAdmin(chatRoom))
            {
                chatRoom.removeAdmin(user);
            }
        }

        public void addUser(ChatRoom chatRoom, User user)
        {
            if (isAdmin(chatRoom) && !isUserInRoom(chatRoom, user))
            {
                chatRoom.addUser(user);
            } else if (!isAdmin(chatRoom))
            {
                Console.WriteLine("You are not an admin of this chat room");
            } else if (isUserInRoom(chatRoom, user))
            {
                Console.WriteLine("User is already in this chat room");
            }
        }

        public void removeUser(ChatRoom chatRoom, User user)
        {
            if (isAdmin(chatRoom))
            {
                chatRoom.removeUser(user);
            }
        }

        public bool isUserInRoom(ChatRoom chatRoom, User user)
        {
            return chatRoom.containUser(user);
        }

        // create room and make this user admin of the room
        public void createRoom(String name)
        {
            ChatRoom chatRoom = new ChatRoom(name, this);
            chatRooms.Add(chatRoom);
            // chatRoom.addUser(this);
            // chatRoom.addAdmin(this);
        }

        public void deleteRoom(ChatRoom chatRoom)
        {
            if (isAdmin(chatRoom))
            {
                chatRoom.deleteRoom();
                // chatRooms.Remove(chatRoom);
            }
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