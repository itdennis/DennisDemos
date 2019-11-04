using System;
using System.Collections.Generic;
using System.Text;

namespace DennisCoreDemos.LeetCodes
{
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int x) { val = x; }
    }
    public class Lc21
    {
        public ListNode MergeTwoLists(ListNode l1, ListNode l2)
        {
            if (l1 == null && l2 != null)
            {
                return l2;
            }

            if (l2 == null && l1 != null)
            {
                return l1;
            }

            if (l1 == null && l2 == null)
                return null;


            ListNode l3;
            if (l1.val <= l2.val)
            {
                l3 = new ListNode(l1.val);
                l1 = l1.next;
            }
            else
            {
                l3 = new ListNode(l2.val);
                l2 = l2.next;
            }
            CompareTwoNodes(l1, l2, ref l3);
            return l3;

        }

        public void CompareTwoNodes(ListNode l1, ListNode l2, ref ListNode l3)
        {
            if (l1 == null && l2 != null) 
            {
                l3.next = l2;
                return;
            }
                
            if (l2 == null && l1 != null)
            {
                l3.next = l1;
                return;
            }
                
            if (l1 == null && l2 == null)
                return;

            if (l1.val <= l2.val)
            {
                l3.next = new ListNode(l1.val);
                l1 = l1.next;
            }
            else
            {
                l3.next = new ListNode(l2.val);
                l2 = l2.next;
            }

            CompareTwoNodes(l1, l2, ref l3.next);
        }
    }
}
