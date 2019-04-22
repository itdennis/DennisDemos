using System;

namespace AccessDemo
{
    class Program
    {
        /// <summary>
        /// 访问者模式, 表示一个作用于某对象结构中的各元素的操作.
        /// 它使你可以在不改变各元素的类的前提下定义这些元素的新操作.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ObjectStructure o = new ObjectStructure();
            o.Add(new Man());
            o.Add(new Woman());

            Success success = new Success();
            o.Display(success);

            
        }
    }
}
