class Solution:
    def nextGreaterElement(self, nums1, nums2):
        stack, hashmap = list(), dict()
        for i in nums2:
            if(len(stack) == 0 or stack[-1] >= i):
                stack.append(i)
            else:
                while len(stack) > 0 and stack[-1] < i:
                    hashmap[stack.pop()] = i
                stack.append(i)
        
        res = list()
        for i in nums1:
            res.append(hashmap.get(i, -1))
        return res