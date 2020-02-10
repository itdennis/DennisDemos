using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.LeetCodes
{
    public class LeetCode290
    {
        public bool WordPattern(string pattern, string str)
        {
            if (pattern == null || str == null)
            {
                return false;
            }

            var strs = str.Split(' ');
            var patterns = pattern.ToCharArray();

            if (strs.Length != patterns.Length)
            {
                return false;
            }

            Dictionary<char, string> cache = new Dictionary<char, string>();
            for (int i = 0; i < patterns.Length; i++)
            {
                if (!cache.ContainsKey(patterns[i]))
                {
                    if (cache.ContainsValue(strs[i]))
                    {
                        return false;
                    }
                    cache.Add(patterns[i], strs[i]);
                }
                else
                {
                    if (!cache[patterns[i]].Equals(strs[i]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
