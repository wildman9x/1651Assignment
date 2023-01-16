using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace _1651Assignment.model
{
    public class ChatRoom : ChatMediator, IDisposable
    {
        private bool disposedValue;

        // [BsonId]
        // [BsonRepresentation(BsonType.ObjectId)]
        // public String Id { get; set; }
        // public String Name { get; set; }
        // public List<User> Users { get; set; }
        // public List<Message> Messages { get; set; }
        public List<User> Admins { get; set; } = new List<User>();

        public ChatRoom(String name)
        {
            Name = name;
            // Id = Guid.NewGuid().ToString();
            // Users = new List<User>();
            // Messages = new List<Message>();
            // Admins = new List<User>();
        }

        public ChatRoom(String name, User user)
        {
            Name = name;
            // Id = Guid.NewGuid().ToString();
            // Users = new List<User>();
            // Messages = new List<Message>();
            // Admins = new List<User>();
            Users.Add(user);
            Admins.Add(user);
            user.addChat(this);
        }

        public override void addMessage(String message, User user)
        {
            Message m = new Message(message, user, this);
            Messages.Add(m);
            notifyAll(m);
        }

        public void showAdmins()
        {
            foreach (User u in Admins)
            {
                Console.WriteLine(u);
            }
        }

        public void showUsers()
        {
            foreach (User u in Users)
            {
                Console.WriteLine(u);
            }
        }

        public void deleteRoom()
        {
            // TODO
            foreach (User u in new List<User>(Users))
            {
                u.leaveRoom(this);
            }
            foreach (User u in new List<User>(Admins))
            {
                u.leaveRoom(this);
            }
            foreach (Message m in new List<Message>(Messages))
            {
                deleteMessage(m);
            }
            this.Dispose();
        }

        public void deleteMessage(Message message)
        {
            Messages.Remove(message);
        }

        public override void removeUser(User userWhoRemove, User userToRemove)
        {
            Users.Remove(userToRemove);
        }

        public override void addUser(User userWhoAdded, User userToAdd)
        {
            if (Admins.Contains(userWhoAdded))
            {
                Users.Add(userToAdd);
            }
            else
            {
                Console.WriteLine("You are not an admin");
            }
        }
        public void addAdmin(User user)
        {
            Admins.Add(user);
        }

        public void removeAdmin(User user)
        {
            Admins.Remove(user);
        }

        public bool containAdmin(User user)
        {
            return Admins.Contains(user);
        }

        public bool containUser(User user)
        {
            return Users.Contains(user);
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

        public override void notifyAll(Message message)
        {
            foreach (User u in Users)
            {
                u.getNotification(message, this);
            }
        }

        public override void displayMessages()
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