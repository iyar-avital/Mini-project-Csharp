using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public static class Validation
    {
        public static bool IsPhoneNumber(string s)
        {
            if (s.Length != 11)
                return false;
            if (s[0] != '0')
                return false;
            foreach (var item in s)
            {
                if (item != '-' && !char.IsDigit(item))
                    return false;
            }
            return true;
        }

        public static bool IsEmail(string s)
        {
            if (s[0] == '@')
                return false;
            if (s.Contains('@') != true || s.Substring(s.IndexOf("@") + 1).Contains(".") != true)
                return false;
            foreach (var item in s)
            {
                if (item != '@' && item != '.' && item != '_' && !char.IsLetter(item) && char.IsWhiteSpace(item))
                    return false;
            }
            return true;
        }

        public static bool IsId(string idnumber)
        {
            if (idnumber.Length != 9)
                return false;
            int[] id = new int[idnumber.Length];
            for (int i = 0; i < idnumber.Length; i++)
                id[i] = idnumber[i] - '0';
            for (int i = 1; i < 8; i = i + 2)
            {
                int a = id[i] * 2;
                if (a > 9)
                    a = (a % 10) + (a / 10);
                id[i] = a;
            }
            int sumOfDigits = 0;
            for (int i = 0; i < id.Length; i++)
                sumOfDigits += id[i];
            return sumOfDigits % 10 == 0;
        }


        public static bool IsName(string s)
        {
            foreach (var item in s)
            {
                if (!char.IsLetter(item) && !char.IsWhiteSpace(item))
                    return false;
            }
            return true;
        }
    }
}
