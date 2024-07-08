using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SocketLib
{
    public static class Logger
    {
        public static void Message(System.Object pMessage)
        {
            Debug.Log($"[ MESSAGE ] : {pMessage}");
        }

        public static void Error(System.Object pError)
        {
            Debug.LogError($"[ ERROR ] : {pError}");
        }
    }
}
