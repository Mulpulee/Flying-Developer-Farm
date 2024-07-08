using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketLib
{
    public static class Dependency<T>
    {
        private static Func<T> m_getter;
        public static void Assign(Func<T> pGetter)
        {
            m_getter = pGetter;
        }

        public static T Get()
        {
            return m_getter();
        }
    }
}
