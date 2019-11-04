using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DennisCoreDemos.LeetCodes
{
    public class Lc15
    {
        public static async Task<bool> Run(string str)
        {
            Task<bool> task = new Task<bool>(() => { return DoOps(str); });
            task.Start();
            return await task;
        }

        private static bool DoOps(string str)
        {
            System.Collections.Generic.Dictionary<char, char> cache = new System.Collections.Generic.Dictionary<char, char>() { { '(', ')' }, { '[', ']' }, { '{', '}' }, };
            System.Collections.Generic.Stack<char> ts = new System.Collections.Generic.Stack<char>();
            for (int i = 0; i < str.Length; i++)
            {
                if (cache.ContainsKey(str[i]))
                {
                    ts.Push(cache[str[i]]);
                }
                else
                {
                    if (ts.Count > 0)
                    {
                        var top = ts.Pop();
                        if (top != str[i])
                        {
                            return false;
                        }
                    }
                    else
                        return false;
                    
                }
            }
            if (ts.Count > 0)
            {
                return false;
            }
            return true;
        }
    }
}
