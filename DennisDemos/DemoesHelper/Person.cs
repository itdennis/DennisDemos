using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.DemoesHelper
{
    //定义了一个从System.Delegate类派生的类
    //也可以理解为一种数据类型 这种数据类型指向返回值为void 参数为Person对象的函数
    //我们也可以把Person类理解为一种数据类型 只是它包含的是Name和Age
    public delegate void EatFoodDelegate(Person p);

    public class Person
    {

        public string Name { get; set; }

        public int Age { get; set; }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        //既然委托是一数据类型和String一样，所以可以像声明String对象一样声明代理变量
        public EatFoodDelegate eatFoodDelegate;
        ////之前是直接声明委托，现在是声明一个事件
        //public event EatFoodDelegate EatFoodEventHandler;

        public void eating()
        {

            if (eatFoodDelegate != null)
            {
                eatFoodDelegate(this);
            }
        }

    }
}
