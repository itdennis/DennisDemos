using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.LeetCodes
{
    public class LeetCode349
    {
        public int[] Intersection(int[] nums1, int[] nums2)
        {
            if (nums1 == null || nums2 == null)
            {
                return null;
            }
            var nums1List = nums1.ToList();
            var nums2List = nums2.ToList();

            return nums1List.Intersect<int>(nums2List).ToArray();
        }
    }
}
