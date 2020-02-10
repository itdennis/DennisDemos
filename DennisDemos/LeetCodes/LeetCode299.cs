using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.LeetCodes
{
    public class LeetCode299
    {
        public string GetHint(string secret, string guess)
        {
            var secretCharList = secret.ToCharArray().ToList();
            int cowCount = 0;
            foreach (var g in guess)
            {
                if (secretCharList.Contains(g))
                {
                    cowCount++;
                    secretCharList.Remove(g);
                }
            }

            var secretCharArray = secret.ToCharArray();
            var guessCharArray = guess.ToCharArray();
            int bullCount = 0;
            for (int i = 0; i < secretCharArray.Length; i++)
            {
                if (secretCharArray[i] == guessCharArray[i])
                {
                    bullCount++;
                }
            }

            return $"{bullCount}A{cowCount-bullCount}B";
        }
    }
}
