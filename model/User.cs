using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace _1651Assignment.model
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public String Name { get; set; } = "";
        public String Phone { get; set; } = "";
        // private password
        public String Password {get; set;} = "";
        public List<ChatMediator> Chats { get; set; } = new List<ChatMediator>();

        // public User()
        // {
        //     Chats = new List<chatMediator>();
        // }

        // public User(String name, String phone, String password)
        // {

        //     Name = name;
        //     Phone = phone;
        //     Password = password;
        //     Chats = new List<chatMediator>();
        // }

        public bool checkPassword(String password)
        {
            return Password.Equals(password);
        }

        // add chat
        public void addChat(ChatMediator chat)
        {
            Chats.Add(chat);
        }

        public void sendMessage(String message, ChatMediator chatRoom)
        {
            chatRoom.addMessage(message, this);
        }

        public void getNotification(Message message, ChatMediator chat)
        {
            // TODO
            Console.WriteLine("User: " + Name + 
            " \n received message: " + message.message + 
            (chat is ChatRoom ? " \n from chat room: " + chat.Name : "") +
            "\n from user: " + message.user.Name +
            "\n");
        }

        public void leaveRoom(ChatRoom chatRoom)
        {
            chatRoom.removeUser(this, this);
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
                chatRoom.addUser(this, user);
            }
            else if (!isAdmin(chatRoom))
            {
                Console.WriteLine("You are not an admin of this chat room");
            }
            else if (isUserInRoom(chatRoom, user))
            {
                Console.WriteLine("User is already in this chat room");
            }
        }

        public void removeUser(ChatRoom chatRoom, User user)
        {
            if (isAdmin(chatRoom))
            {
                chatRoom.removeUser(this, user);
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
            Chats.Add(chatRoom);
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

        // find chatOne with another user
        public ChatMediator? findChatOne(User user)
        {
            if (user == this || user == null || Chats == null)
            {
                return null;
            }
            foreach (ChatMediator chat in Chats)
            {
                if (chat is ChatOne)
                {
                    if (((ChatOne) chat).containUser(user))
                    {
                        return chat;
                    }
                }
            }
            return null;
        }

        // send chatOne message to another user, if there is no chatOne with that user, create one
        public void sendChatOneMessage(String message, User user)
        {
            ChatMediator? chat = findChatOne(user);
            // if Chats is null, create a new list
            
            if (!(chat != null))
            {
                try
                {
                    ChatOne chatOne = new ChatOne(this, user);
                    chat = chatOne;
                    Chats.Add(chat);
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            chat.addMessage(message, this);
        }
    }
}