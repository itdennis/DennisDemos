using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.LeetCodes
{
   public class LeetCode389
    {
        public char FindTheDifference(string s, string t)
        {
            Dictionary<char, int> cache = new Dictionary<char, int>();
            for (int i = 0; i < s.Length; i++)
            {
                if (cache.ContainsKey(s[i]))
                {
                    cache[s[i]]++;
                }
                else
                {
                    cache.Add(s[i], 1);
                }
            }
            char res = ' ';
            for (int i = 0; i < t.Length; i++)
            {
                if (!cache.ContainsKey(t[i]))
                {
                    res = t[i];
                    break;
                }

                if (cache.ContainsKey(t[i]) && cache[t[i]] > 0)
                {
                    cache[t[i]]--;
                }
                else
                {
                    res = t[i];
                    break;
                }
            }
            return res;
        }
    }
}
