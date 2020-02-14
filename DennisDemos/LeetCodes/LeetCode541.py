class Solution:
    def reverseStr(self, s, k):
        res = ''
        lenOfs = len(s) # s = 1234567890 k = 2
        while lenOfs > 0:
            if lenOfs >= 2 * k:
                res += ''.join(reversed(s[0: 2*k][0: k])) 
                res += s[0: 2*k][k : 2*k]
            if lenOfs >= k and lenOfs < 2*k:
                res += ''.join(reversed(s[0: k])) 
                res += s[k: lenOfs]
                break
            if lenOfs  < k:
                res += ''.join(reversed(s)) 
                break
            s = s[2*k: lenOfs]
            lenOfs = len(s)
        return res

s = Solution()
res =s.reverseStr("1234567890", 2)
print(res)