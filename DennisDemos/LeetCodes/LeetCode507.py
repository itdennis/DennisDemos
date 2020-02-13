class Solution:
    def checkPerfectNumber(self, num):
        if num <= 0:
            return False

        n = 1
        resSum = 0
        while n * n <= num:
            if num % n == 0:
                resSum += n
                if n * n != num:
                    resSum += num / n

            n+=1
        return num == resSum - num


s = Solution()
s.checkPerfectNumber(6)
