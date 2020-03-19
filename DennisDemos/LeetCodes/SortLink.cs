using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.LeetCodes
{
    public class SortLink
    {
        // 3->5->7->1->6
        public void Sort(ListNode node) 
        {
            ListNode i = null;
            //ListNode j = null;
            //ListNode x = null;

            if (node == null || node.next == null)
            {
                return;
            }

            for ( i = node; i != null; i = i.next)
            {
                for (ListNode j = node; j != null; j = j.next)
                {
                    if (i.val > j.val)
                    {
                        var temp = j;
                        j.val = i.val;
                        i.val = temp.val;
                    }
                }
            }
        }
    }
}
