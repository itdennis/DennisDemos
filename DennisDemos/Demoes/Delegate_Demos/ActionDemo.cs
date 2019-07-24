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

            Action action1 = new Action(DelegateFunction2);
            this.RunAction(action1);
        }

        public void DelegateFunction(string str, ref string sth)
        {
            sth = "yes, she is.";
            Console.WriteLine(str);
        }

        public void DelegateFunction2(string str, ref string sth)
        {
            sth = "no, xxxxxxxxx.";
            Console.WriteLine(str);
            // other logic
        }

        public void RunAction(Action action)
        {
            string res = "";
            action("d", ref res);
            action.Invoke("bear2 is a pig", ref res);
            Console.WriteLine(res);
        }
    }
}
