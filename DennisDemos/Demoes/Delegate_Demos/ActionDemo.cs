using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Demoes.Delegate_Demos
{
    public class ActionDemo
    {
        public delegate void Action(string str, ref string str2);
        public void Run()
        {
            Action action = new Action(DelegateFunction);
            this.RunAction(action);
        }

        public void DelegateFunction(string str, ref string sth)
        {
            sth = "yes, she is.";
            Console.WriteLine(str);
        }
        
        public void RunAction(Action action)
        {
            string res = "";
            action.Invoke("bear2 is a pig", ref res);
            Console.WriteLine(res);
        }
    }
}
