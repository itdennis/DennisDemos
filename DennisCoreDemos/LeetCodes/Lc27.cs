using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DennisCoreDemos.LeetCodes
{
    public class Lc27
    {
        public static async Task<int> Run(int[] nums, int val)
        {
            Task<int> task = new Task<int>(() => { return DoOps(nums, val); });
            task.Start();
            return await task;
        }

        private static int DoOps(int[] nums, int val)
        {
            if (nums.Length == 0)
            {
                return 0;
            }
            int j = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                while (nums[i] == val)
                {
                    var old = i;
                    i++;
                    if (i >= nums.Length)
                    {
                        break;
                    }
                    nums[old] = nums[i];
                }
                if (i < nums.Length && nums[i] != val)
                {
                    j++;
                }
            }
            return j;
        }
    }
}
