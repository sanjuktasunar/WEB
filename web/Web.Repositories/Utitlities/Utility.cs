using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Repositories.Utitlities
{
    public static class Utility
    {
        public static string Generate6DRandomNumber()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string str= new string(Enumerable.Repeat(chars, 6)
                 .Select(s => s[random.Next(s.Length)]).ToArray());
            return str;
        }
    }
}

