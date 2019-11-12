using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DennisCoreDemos.LeetCodes
{
    public class Lc53
    {
        public static async Task<int> Run(int[] nums)
        {
            Task<int> task = new Task<int>(() => { return MaxSubArray(nums); });
            task.Start();
            return await task;
        }

        private static int DoOps(int[] nums)
        {
            if (nums.Length == 0)
            {
                return 0;
            }

            int? maxSum = null;
            for (int i = 1; i < nums.Length + 1; i++)
            {
                int? iSum = null;
                for (int j = 0; j < nums.Length; j++)
                {
                    int countWorker = 0;
                    int jSum = 0;
                    int z = j;
                    if (nums.Length - j < i)
                    {
                        break;
                    }
                    while (countWorker < i)
                    {
                        countWorker++;
                        jSum += nums[z];
                        z++;
                    }
                    if (iSum == null || iSum < jSum)
                    {
                        iSum = jSum;
                    }
                }
                if (maxSum == null || maxSum < iSum)
                {
                    maxSum = (int)iSum;
                }
            }
            return (int)maxSum;
        }

        private static int MaxSubArray(int[] nums) 
        {
            if (nums.Length == 0)
            {
                return 0;
            }
            int max = nums[0];
            int temp = 0;
            foreach (var num in nums)
            {
                //如果temp与下一个num相加没有比下一个num本身大, 那证明之前的序列已经完犊子了, 最大都没超过下个num.
                if (temp + num > num)
                {
                    temp += num;
                }
                else
                {
                    //在这里直接将temp改成num继续循环
                    temp = num;
                }
                //当前temp和之前记录的max值比较一下, 将两者比较大的值存起来
                max = Math.Max(max, temp);
            }
            return max;
        }
    }
}
