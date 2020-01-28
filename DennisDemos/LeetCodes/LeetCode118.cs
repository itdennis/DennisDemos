using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.LeetCodes
{
    public class LeetCode118
    {
        public IList<IList<int>> Generate(int numRows)
        {
            IList<IList<int>> res = new List<IList<int>>();
            for (int i = 1; i <= numRows; i++)
            {
                List<int> row = new List<int>();

                for (int j = 0; j < i; j++)
                {
                    if (j == 0 || j == i-1)
                    {
                        row.Add(1);
                    }
                    else
                    {
                        row.Add(res[i - 2][j - 1] + res[i - 2][j]);
                    }
                }

                res.Add(row);
            }

            return res;
        }
    }
}
