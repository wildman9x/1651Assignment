using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1651Assignment.model
{
    public class Admin : User
    {
        public Admin(String name, String phone, String password) : base(name, phone, password)
        {
        }

        public void deleteRoom(ChatRoom chatRoom)
        {
            // TODO
            foreach (User user in chatRoom.Users)
            {
                user.chatRooms.Remove(chatRoom);
            }
            chatRoom.Messages.Clear();
            chatRoom.Users.Clear();
            chatRoom.Admins.Clear();
            chatRoom.Dispose();
        }

        public void addMember(User user, ChatRoom chatRoom)
        {
            // TODO
            chatRoom.Users.Add(user);
        }

        public void removeMember(User user, ChatRoom chatRoom)
        {
            // TODO
            chatRoom.Users.Remove(user);
        }

        public void makeAdmin(User user, ChatRoom chatRoom)
        {
            // TODO
            chatRoom.Admins.Add(user);
        }
    }
}