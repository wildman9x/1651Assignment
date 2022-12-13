using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1651Assignment.model
{
    public interface chatMediator
    {
        public void addMessage(String message, User user);
        public void addUser(User user);
        public void removeUser(User user);
        
        public String Name { get; set; }
        public void notifyAll(String message, User user);
        public void displayMessages();
    }
}