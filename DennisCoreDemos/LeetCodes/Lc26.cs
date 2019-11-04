using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DennisCoreDemos.LeetCodes
{
    public class Lc26
    {
        public static async Task<int> Run(int[] nums)
        {
            Task<int> task = new Task<int>(() => { return DoOps(nums); });
            task.Start();
            return await task;
        }
        /// <summary>
        /// nums = [0,0,1,1,1,2,2,3,3,4],
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        private static int DoOps(int[] nums)
        {
            if (nums.Length == 0)
            {
                return 0;
            }

            int i = 0;
            for (int j = 1; j < nums.Length; j++)
            {
                if (nums[i] != nums[j])
                {
                    i++;
                    nums[i] = nums[j];
                }
            }
            return i + 1;
        } 
    }
}
