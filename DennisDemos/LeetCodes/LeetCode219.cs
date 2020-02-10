using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.LeetCodes
{
    public class LeetCode219
    {
        /// <summary>
        /// 1. create a cache to store the index that the value equals k;
        /// 2. loop the nums array, and store the value index.
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public bool ContainsNearbyDuplicate(int[] nums, int k)
        {
            Dictionary<int, int> cache = new Dictionary<int, int>();

            for (int i = 0; i < nums.Length; i++)
            {
                if (!cache.Keys.Contains(nums[i]))
                {
                    cache.Add(nums[i], i);
                }
                else
                {
                    if (i - cache[nums[i]] <= k)
                    {
                        return true;
                    }
                    else
                    {
                        cache.Remove(nums[i]);
                        cache.Add(nums[i], i);
                    }
                }
            }
            return false;
        }
    }
}
