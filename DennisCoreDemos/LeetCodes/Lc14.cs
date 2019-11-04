using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DennisCoreDemos.LeetCodes
{
    public class Lc14
    {
        public static async Task<string> Run(string[] strs)
        {
            Task<string> task = new Task<string>(() => { return DoOps2(strs); });
            task.Start();
            return await task;
        }

        private static string DoOperations(string[] strs)
        {
            if (strs.Length == 0) return "";
            String prefix = strs[0];
            for (int i = 1; i < strs.Length; i++)
                while (strs[i].IndexOf(prefix) != 0)
                {
                    prefix = prefix.Substring(0, prefix.Length - 1);
                    if (prefix.Equals(string.Empty)) return "";
                }
            return prefix;
        }

        private static string DoOps2(string[] strs) 
        {
            if (strs == null || strs.Length == 0)
            {
                return "";
            }
            for (int i = 0; i < strs[0].Length; i++)
            {
                char c = strs[0][i];
                for (int j = 1; j < strs.Length; j++)
                {
                    if (i == strs[j].Length || strs[j][i] != c)
                    {
                        return strs[0].Substring(0, i);
                    }
                }
            }
            return strs[0];
        }
    }
}
