using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1651Assignment.model
{
    public class ChatRoom : chatMediator, IDisposable
    {
        private bool disposedValue;

        public String ID { get; set; }
        public String Name { get; set; }
        public List<User> Users { get; set; }
        public List<Message> Messages { get; set; }
        public List<User> Admins { get; set; }

        public ChatRoom(String name)
        {
            Name = name;
            Users = new List<User>();
            Messages = new List<Message>();
            Admins = new List<User>();
        }

        public ChatRoom(String name, User user)
        {
            Name = name;
            Users = new List<User>();
            Messages = new List<Message>();
            Admins = new List<User>();
            Users.Add(user);
            Admins.Add(user);
        }

        public void addMessage(String message, User user)
        {
            Messages.Add(new Message {message = message, user = user, time = DateTime.Now});
            notifyAll(message, user);
        }

        

        public void messageSearch(String message)
        {
            // TODO
            foreach (Message m in Messages)
            {
                if (m.message.Contains(message))
                {
                    Console.WriteLine(m);
                }
            }
        }

        public void notifyAll(String message, User user)
        {
            foreach (User u in Users)
            {
                u.getNotification(message, this);
            }
        }

        public void displayMessages()
        {
            foreach (Message m in Messages)
            {
                Console.WriteLine(m);
            }
        }


        public override string ToString()
        {
            // return "ChatRoom{" +
            //         "Name='" + Name + '\'' +
            //         ", Users=" + Users +
            //         ", Messages=" + Messages +
            //         ", Admins=" + Admins +
            //         '}';

            String result = "ChatRoom{" +
                            "Name='" + Name + '\'' +
                            "\n Users=";
            foreach (User u in Users)
            {
                result += u.Name + ", ";
            }
            result += "\n Messages=";
            foreach (Message m in Messages)
            {
                result += m + ", ";
            }
            result += "\n Admins=";
            foreach (User u in Admins)
            {
                result += u.Name + ", ";
            }
            result += '}';
            return result;
        }

        public String getName()
        {
            return Name;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ChatRoom()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}