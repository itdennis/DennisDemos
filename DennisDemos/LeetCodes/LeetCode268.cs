using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.LeetCodes
{
    public class LeetCode268
    {
        public int MissingNumber(int[] nums)
        {
            HashSet<int> test = new HashSet<int>();

            for (int i = 0; i < nums.Length + 1; i++)
            {
                test.Add(i);
            }

            for (int i = 0; i < nums.Length; i++)
            {
                if (test.Contains(nums[i]))
                {
                    test.Remove(nums[i]);
                }
            }
            return test.First() ;
        }
    }
}
