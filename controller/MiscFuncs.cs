using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1651Assignment.controller
{
    public class MiscFuncs
    {
        // random string of digit number, take in length
        public static String randomStringDigit(int length)
        {
            Random random = new Random();
            String s = "";
            for (int i = 0; i < length; i++)
            {
                s += random.Next(0, 9);
            }
            return s;
        }
    }
}