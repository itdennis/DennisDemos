using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DennisCoreDemos.LeetCodes
{
    public class Lc13
    {
        public static async Task<int> Run(string input) 
        {
            Task<int> task = new Task<int>(() => { return DoOperations(input); });
            task.Start();
            return await task;
        }

        private static int DoOperations(string input) 
        {
            Dictionary<char, int> cache = new Dictionary<char, int>() { { 'I', 1 },  { 'V', 5 },  { 'X', 10 }, { 'L', 50 }, { 'C', 100 }, { 'D', 500 }, { 'M', 1000 } };
            int ans = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if ((i + 1 == input.Length) || (cache[input[i]] >= cache[input[i + 1]]))
                    ans += cache[input[i]];
                else
                    ans -= cache[input[i]];
            }

            return ans;
        }
    }
}
