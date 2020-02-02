using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.LeetCodes
{
    public class LeetCode203
    {
        public ListNode RemoveElements(ListNode head, int val)
        {
            if (head == null)
                return head;
            head.next = RemoveElements(head.next, val);
            return head.val == val ? head.next : head ;
        }
    }
}
