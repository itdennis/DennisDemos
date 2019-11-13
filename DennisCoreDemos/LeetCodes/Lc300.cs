using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DennisCoreDemos.LeetCodes
{
    public class Lc300
    {
        public static async Task<int> Run(int[] nums)
        {
            Task<int> task = new Task<int>(() => { return DoOps(nums); });
            task.Start();
            return await task;
        }

        private static int DoOps(int[] nums)
        {
            if (nums.Length == 0) return 0;
            int[] dp = new int[nums.Length];
            int res = 0;
            Array.Fill(dp, 1);
            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (nums[j] < nums[i])
                        dp[i] = Math.Max(dp[i], dp[j] + 1);
                        //dp[j]++;
                }
                res = Math.Max(res, dp[i]);
            }
            return res;

        }
    }
}
