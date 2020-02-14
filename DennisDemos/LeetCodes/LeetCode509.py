class Solution:
    def fib(self, N):
        if N == 0:
            return 0

        if N == 1:
            return 1
        
        return self.fib(N-1) + self.fib(N-2)

s = Solution()
res = s.fib(4)
print(res)