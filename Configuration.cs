using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public static class Configuration
    {
        public static int GuestRequestKey = 10000000;
        public static int BankNumber = 0;
        public static int BranchNumber = 111;
        public static int HostingUnitKey = 10000000;
        public static int OrderKey = 20000000;
        private static int commission = 10;
        private static DateTime lastDateThread = new DateTime();
        public static int Commission { get { return commission; } set { commission = value; } }
        public static DateTime LastDateThread { get { return lastDateThread; } set { lastDateThread = value; } }
    }
}
