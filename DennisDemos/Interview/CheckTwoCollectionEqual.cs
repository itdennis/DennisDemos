using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Interview
{
    public class CheckTwoCollectionEqual
    {
        #region Demo
        static void Demo()
        {
            var now = DateTimeOffset.UtcNow.DateTime.ToShortDateString();
            List<string> lA = new List<string>() { "111", "222", "222", "333" };
            List<string> lB = new List<string>() { "111", "222", "333", "333" };
            if (ScrambledEquals(lA, lB))
            {
                Console.WriteLine($"la and lb is ScrambledEquals equal.");
            }
            if (lA.All(lB.Contains))
            {
                Console.WriteLine($"la and lb is All equal.");
            }
            if (lA.SequenceEqual(lB))
            {
                Console.WriteLine($"la and lb is SequenceEqual.");
            }

            Dictionary<string, string> dA = new Dictionary<string, string>() { { "111", "value" }, { "222", "value" }, { "333", "value" }, { "444", "value" }, { "555", "value" } };
            Dictionary<string, string> dB = new Dictionary<string, string>() { { "111", "value" }, { "222", "value" }, { "333", "value" }, { "444", "value" }, { "555", "value" } };

            if (dA.SequenceEqual(dB))
            {
                Console.WriteLine($"da and db is equal.");
            }
        }

        public static bool ScrambledEquals<T>(IEnumerable<T> list1, IEnumerable<T> list2)
        {
            var cnt = new Dictionary<T, int>();
            foreach (T s in list1)
            {
                if (cnt.ContainsKey(s))
                {
                    cnt[s]++;
                }
                else
                {
                    cnt.Add(s, 1);
                }
            }
            foreach (T s in list2)
            {
                if (cnt.ContainsKey(s))
                {
                    cnt[s]--;
                }
                else
                {
                    return false;
                }
            }
            return cnt.Values.All(c => c == 0);
        }


        public static bool ScrambledEquals<T>(IEnumerable<T> list1, IEnumerable<T> list2, IEqualityComparer<T> comparer)
        {
            var cnt = new Dictionary<T, int>(comparer);
            return false;
        }
        #endregion

        
    }
}
