class Solution:
    def checkRecord(self, s):
        sList = s.split('A')
        if len(sList) > 2:
            return False
        sList = s.split('LL')
        if len(sList) > 1:
            return False

        return True


s = Solution()
s.checkRecord("PPALLP")
