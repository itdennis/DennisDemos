using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.LeetCodes
{
    public class LeetCode234
    {
        public bool IsPalindrome(ListNode head)
        {
            if (head == null || head.next == null)
            {
                return true;
            }

            //find the middle node
            ListNode fast = head;
            ListNode slow = head;
            while (fast.next != null && fast.next.next != null)
            {
                fast = fast.next.next;
                slow = slow.next;
            }
            slow = ReverseListNode(slow.next);
            while (slow != null)
            {
                if (head.val != slow.val)
                {
                    return false;
                }
                slow = slow.next;
                head = head.next;
            }
            return true;
        }

        private ListNode ReverseListNode(ListNode node)
        {
            ListNode pre = null;
            ListNode cur = node;
            while (cur != null)
            {
                ListNode temp = cur.next;
                cur.next = pre;
                pre = cur;
                cur = temp;
            }
            return pre;
        }
    }
}
