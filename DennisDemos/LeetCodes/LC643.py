class Solution:
    def findMaxAverage(self, nums, k):
        m = 0
        for index in range(len(nums) - k + 1):
            curr = 0
            for a in range(k):
                curr += nums[index + a]
            if index == 0:
                m = curr
            else:
                m = max(m, curr)
        return m / k


s = Solution()
s.findMaxAverage([-1], 1)