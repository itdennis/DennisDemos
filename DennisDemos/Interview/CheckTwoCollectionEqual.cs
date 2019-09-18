using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Interview
{
    public class CheckTwoCollectionEqual
    {
        public static void Demo()
        {
            var now = DateTimeOffset.UtcNow.DateTime.ToShortDateString();
            List<string> lA = new List<string>() { "111", "333", "222" };
            List<string> lB = new List<string>() { "111", "222", "333" };

            var result = lA.Select(lla => lla).OrderByDescending(lla => lla).ToList();

            int[] a = { 1, 2, 3, 4 };
            int[] b = { 1, 4, 3, 2 };
            Console.WriteLine($"int SequenceEqual: {a.SequenceEqual(b)}");

            List<int> la = new List<int>() { 1, 2, 3, 4 };
            List<int> lb = new List<int>() { 1, 4, 3, 2 };
            Console.WriteLine(la.SequenceEqual(lb));



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
                Console.WriteLine($"SequenceEqual ");
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

        
    }
}
