using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.LeetCodes
{
    public class LeetCode136
    {
        public int SingleNumber(int[] nums)
        {
            var res = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                res = res ^ nums[i];
            }
            return res;
        }
    }
}
