class Solution:
    def findRelativeRanks(self, nums):
        sort_dict = { v:i+1 for i, v in enumerate(sorted(nums, reverse=True))}
        gold_dict = {1:"Gold Medal", 2:"Silver Medal",3:"Bronze Medal"}
        
        res = []

        for i in nums:
            res.append(gold_dict[sort_dict[i]] if sort_dict[i] in gold_dict else str(sort_dict[i]))

        return res


s = Solution()
s.findRelativeRanks([1,6,3,4,5,2])