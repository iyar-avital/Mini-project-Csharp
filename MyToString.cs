using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public static class MyToString
    {
        public static string OurSmartToString<T>(this T t)
        {
            string str = "";
            foreach (var item in t.GetType().GetProperties())
            {
                str += item.Name + ": " + item.GetValue(t, null) + "\n";
            }
            return str;
        }
    }
}
