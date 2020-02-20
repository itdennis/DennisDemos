class Solution:
    def findErrorNums(self, nums):
        currSum = 0
        acSum = 0
        for num in nums:
            currSum += num
        
        for index in range(len(nums)+1):
            acSum += index

        mis = abs(acSum - currSum)
        dup = acSum - sum(set(nums))
        if acSum - currSum > 0:
            return [dup - mis, dup]
        else:
            return [dup + mis, dup]

s = Solution()
s.findErrorNums([8,7,3,5,3,6,1,4])