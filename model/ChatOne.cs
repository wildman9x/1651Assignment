using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1651Assignment.model
{
    public class ChatOne : ChatMediator, IDisposable
    {
        private bool disposedValue;

        // public string Name { get ; set ; }

        // public List<User> Users { get; set; }

        // public List<Message> Messages { get; set; }

        public ChatOne(User user1, User user2)
        {
            // Users = new List<User>();
            Users.Add(user1);
            Users.Add(user2);
            Name = user1.Name + " and " + user2.Name;
            // Messages = new List<Message>();
        }

        public override void addMessage(string message, User user)
        {
            Message m = new Message(message, user, this);
            Messages.Add(m);
            notifyAll(m);
        }

        public override void addUser(User userWhoAdded, User userToAdd)
        {
            // create a new chat room
            // room name is all the user in this chat and the userToAdd
            string roomName = createRoomName(userToAdd);
            ChatRoom chatRoom = new ChatRoom(roomName, userWhoAdded);

        }

        private string createRoomName(User userToAdd)
        {
            String roomName = "";
            foreach (User u in Users)
            {
                roomName += u.Name;
            }
            roomName += userToAdd.Name;
            return roomName;
        }

        public override void displayMessages()
        {
            throw new NotImplementedException();
        }

        public override void notifyAll(Message message)
        {
            foreach (User u in Users)
            {
                u.getNotification(message, this);
            }
        }

        public override void removeUser(User userWhoRemove, User userToRemove)
        {
            // if the userToRemove is not userWhoRemove then return
            if (userToRemove != userWhoRemove)
            {
                return;
            }
            // if user list is empty then remove all messages
            if (Users.Count == 0)
            {
                Messages.Clear();
                this.Dispose();
            }
            else
            {
                // remove all messages from this user
                foreach (Message m in Messages)
                {
                    if (m.user == userToRemove)
                    {
                        Messages.Remove(m);
                    }
                }
                // remove this user from user list
                Users.Remove(userToRemove);
            }
        }

        // toString
        public override string ToString()
        {
            return "ChatOne: " + Name;
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
        // ~ChatOne()
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

        internal bool containUser(User user)
        {
            foreach (User u in Users)
            {
                if (u == user)
                {
                    return true;
                }
            }
            return false;
        }
    }
}