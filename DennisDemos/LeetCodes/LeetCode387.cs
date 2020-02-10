using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.LeetCodes
{
    public class LeetCode387
    {
        public int FirstUniqChar(string s)
        {
            if (s == null)
            {
                return 0;
            }
            var charArray = s.ToArray();
            Dictionary<char, int> cache = new Dictionary<char, int>();

            for (int i = 0; i < charArray.Length; i++)
            {
                if (cache.ContainsKey(charArray[i]))
                {
                    cache[charArray[i]] = -1;
                }
                else
                {
                    cache[charArray[i]] = i;
                }
            }
            int res = -1;
            foreach (var item in cache)
            {
                if (item.Value != -1 && res == -1)
                {
                    res = item.Value;
                }

                if (item.Value != -1 && item.Value < res)
                {
                    res = item.Value;
                }
            }

            return res;
        }
    }
}
