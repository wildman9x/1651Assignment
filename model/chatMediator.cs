using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1651Assignment.model
{
    public interface chatMediator
    {
        public void addMessage(String message, User user);
        public void addUser(User userWhoAdded, User userToAdd);
        public void removeUser(User userWhoRemove, User userToRemove);
        
        public String Name { get; set; }

        public void notifyAll(Message message);
        public void displayMessages();
    }
}