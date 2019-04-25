using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Demoes.XiaoJuHua
{
   
    public class Delegate_Lesson
    {
        private string name;
        //public string Name { get => this.name; set => this.name = value; }
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }


        public void GaoJiInvoke(IAsyncResult asyncResult)
        {

        }

        public string BuyApple(string number)
        {
            System.Threading.Thread.Sleep(100000);
            return "no apple.";
        }

        public void RunWithNothing()
        {
            System.Threading.Thread.Sleep(100000);
            Console.WriteLine("go to buy a pen.");
        }

        public void BuyPen(string money)
        {
            this.name = money;
            Console.WriteLine("this is run function.");
        }

        public void BuyPenpen(string money, string number)
        { }

        public void Run(string n)
        {
            this.name = n;
            Console.WriteLine("this is run function.");
        }
    }

    public class Delegate_Lesson_Bak
    {
        private string name;
        //public string Name { get => this.name; set => this.name = value; }
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public void Run(string n)
        {
            this.name = n;
        }
    }

    
}
