using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketLib
{
    public static class Logger
    {
        public static void Message(Object pMessage)
        {
            Console.WriteLine($"[ MESSAGE ] : {pMessage}");
        }

        public static void Error(Object pError)
        {
            Console.WriteLine($"[ ERROR ] : {pError}");
        }
    }
}
