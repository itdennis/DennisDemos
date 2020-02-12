class Solution:
    def findMaxConsecutiveOnes(self, nums) -> int:
        res = 0
        tempCount = 0

        for i in nums:
            if(i == 1):
                tempCount += 1
            else:
                if(tempCount >= len(nums) /2):
                    res = tempCount
                    break
                if(tempCount > res):
                    res = tempCount
                tempCount = 0
        if(tempCount > res):
            res = tempCount
        return res

s = Solution()
res = s.findMaxConsecutiveOnes([1,0,1,1,0,1])
print(res)