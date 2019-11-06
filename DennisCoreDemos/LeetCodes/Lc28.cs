using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DennisCoreDemos.LeetCodes
{
    public class Lc28
    {
        public static async Task<int> Run(string haystack, string needle)
        {
            Task<int> task = new Task<int>(() => { return DoOps(haystack, needle); });
            task.Start();
            return await task;
        }

        private static int DoOps(string haystack, string needle)
        {
            if (needle == "")
            {
                return 0;
            }

            if (haystack == "")
            {
                return -1;
            }

            if (haystack.Contains(needle))
            {
                return haystack.IndexOf(needle, 0);
            }
            else
            {
                return -1;
            }
        }
    }
}
