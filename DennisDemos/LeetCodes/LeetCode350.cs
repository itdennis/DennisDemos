using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.LeetCodes
{
    public class LeetCode350
    {
        public int[] Intersect(int[] nums1, int[] nums2)
        {
            if (nums1 == null || nums2 == null)
            {
                return null;
            }
            Dictionary<int, int> numCounter = new Dictionary<int, int>();
            List<int> resList = new List<int>();
            if (nums1.Length <= nums2.Length)
            {
                for (int i = 0; i < nums1.Length; i++)
                {
                    if (numCounter.ContainsKey(nums1[i]))
                    {
                        numCounter[nums1[i]]++;
                    }
                    else
                    {
                        numCounter.Add(nums1[i], 1);
                    }
                }

                for (int i = 0; i < nums2.Length; i++)
                {
                    if (numCounter.ContainsKey(nums2[i]))
                    {
                        if (numCounter[nums2[i]] > 0)
                        {
                            resList.Add(nums2[i]);
                            numCounter[nums2[i]]--;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < nums2.Length; i++)
                {
                    if (numCounter.ContainsKey(nums2[i]))
                    {
                        numCounter[nums2[i]]++;
                    }
                    else
                    {
                        numCounter.Add(nums2[i], 1);
                    }
                }
                for (int i = 0; i < nums1.Length; i++)
                {
                    if (numCounter.ContainsKey(nums1[i]))
                    {
                        if (numCounter[nums1[i]] > 0)
                        {
                            resList.Add(nums1[i]);
                            numCounter[nums1[i]]--;
                        }
                    }
                }
            }
            return resList.ToArray();
        }
    }
}
