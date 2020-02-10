using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.LeetCodes
{
    public class LeetCode409
    {
        public int LongestPalindrome(string s)
        {
            Dictionary<char, int> cache = new Dictionary<char, int>();

            var sCharArray = s.ToCharArray().ToList();

            foreach (var sChar in sCharArray)
            {
                if (cache.ContainsKey(sChar))
                {
                    cache[sChar]++;
                }
                else
                {
                    cache.Add(sChar, 1);
                }
            }

            int res = 0;
            bool hasSingle = false;
            foreach (var item in cache)
            {
                if (item.Value % 2 != 0)
                {
                    res += item.Value - 1;
                    hasSingle = true;
                }
                else
                {
                    res += item.Value;
                }
            }
            return hasSingle ? res + 1 : res ;
        }
    }
}
