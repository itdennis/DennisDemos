using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.LeetCodes
{
    public class StringToArray_Solution
    {
        public string[] StringToArray(string input) 
        {
            List<string> arrayList = new List<string>();
            string element = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == ' ' && ! string.IsNullOrEmpty(element))
                {
                    arrayList.Add(element);
                    element = "";
                }
                else if(input[i] != ' ')
                {
                    element += input[i];
                }
            }
            if (!string.IsNullOrEmpty(element))
            {
                arrayList.Add(element);
            }
            return arrayList.ToArray();
        }
    }
}
