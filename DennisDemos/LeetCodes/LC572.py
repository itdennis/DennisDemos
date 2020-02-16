class Solution:
    def isSubtree(self, s, t):
        if s == None and t == None:
            return True
        if s == None or t == None:
            return False

        def isSameTree(s, t):
            if s == None and t == None: return True
            return s and t and s.val == t.val and isSameTree(s.left, t.left) and isSameTree(s.right, t.right)

        return self.isSubtree(s.left, t) or self.isSubtree(s.right, t) or isSameTree(s, t)

