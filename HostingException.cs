using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class HostingException : Exception
    {
        public readonly int key;
        public readonly string nameObject;
        public HostingException(string mes, int k, string name) : base(mes)
        {
            key = k;
            nameObject = name;
        }
    }
}
