using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace _1651Assignment.model
{
    public abstract class ChatMediator
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public List<User> Users { get; set; } = new List<User>();

        public List<Message> Messages { get; set; } = new List<Message>();
        public abstract void addMessage(String message, User user);
        public abstract void addUser(User userWhoAdded, User userToAdd);
        public abstract void removeUser(User userWhoRemove, User userToRemove);
        
        public String Name { get; set; } = "";

        public abstract void notifyAll(Message message);
        public abstract void displayMessages();
    }
}