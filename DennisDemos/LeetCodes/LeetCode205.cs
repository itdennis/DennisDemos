using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.LeetCodes
{
    public class LeetCode205
    {
        public bool IsIsomorphic(string s, string t)
        {
            return IsIsomorphicHelper(s, t) && IsIsomorphicHelper(t, s);
        }

        bool IsIsomorphicHelper(string s, string t) 
        {
            if (s.Length != t.Length)
            {
                return false;
            }

            Dictionary<char, char> map = new Dictionary<char, char>();

            var sCharArray = s.ToCharArray();
            var tCharArray = t.ToCharArray();


            for (int i = 0; i < sCharArray.Length; i++)
            {
                if (!map.ContainsKey(sCharArray[i]))
                {
                    map.Add(sCharArray[i], tCharArray[i]);
                }
                else
                {
                    if (map[sCharArray[i]] != tCharArray[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
