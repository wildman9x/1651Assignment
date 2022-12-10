using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1651Assignment.model
{
    public class Message
    {
        public String message { get; set; }
        public User user { get; set; }
        public DateTime time { get; set; }

        public override string ToString()
        {
            // display user, time, message
            return "User: " + user.Name + "\n"
                    + "Time: " + time + "\n"
                    + "Message: " + message + "\n";
        }
    }
}