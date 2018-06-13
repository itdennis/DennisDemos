using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DennisDemos.DemosHelper;

namespace DennisDemos.Demos
{
    public class DelegateDemo
    {
        /// <summary>
        /// chineseEat 实际上是一个独立的方法, 需要和委托变量关联起来, 但是程序运行过程中, chineseEat实际上只被调用了一次. 
        /// 对于程序比较复杂的代码来说, 这种情况会使得代码的结构比较混乱, 而且可读性比较差.
        /// 因此对于那种在程序运行中只调用一次的委托方法, 建议使用 匿名方法的方式去调用.
        /// 匿名方法 , 顾名思义就是说方法没有名字. 所以这种匿名方法必须在定义的时候进行调用. 并没有重用性.
        /// </summary>
        public void Run()
        {
            Person chinesePerson = new Person("小明", 25);
            //普通委托示例
            chinesePerson.eatFoodDelegate += new EatFoodDelegate(chineseEat); // += 将方法注册到委托当中.
            chinesePerson.eating();

            Console.WriteLine("--------------------------------------");

            //使用匿名函数示例
            EatFoodDelegate EnglishEatPisaDelegate = delegate (Person p)
            {
                Console.WriteLine("I'm {0},I am {1} , I eat PiSa", p.Name, p.Age);
            };

            Person englishPerson = new Person("Ivan", 25);
            englishPerson.eatFoodDelegate = delegate (Person p)
            {
                Console.WriteLine("I'm {0},I am {1} , I eat MianBao", p.Name, p.Age);
            };
            englishPerson.eatFoodDelegate += EnglishEatPisaDelegate;
            englishPerson.eating();

            Console.WriteLine("--------------------------------------");

            //lambda示例
            Person lambdaPerson = new Person("lambda", 25);
            lambdaPerson.eatFoodDelegate = (Person p) => {
                Console.WriteLine("I'm {0},I am {1} , I eat MianBao1", p.Name, p.Age);
            };
            lambdaPerson.eatFoodDelegate += (Person p) => {
                Console.WriteLine("I'm {0},I am {1} , I eat PiSa1", p.Name, p.Age);
            };
            lambdaPerson.eating();
            Console.ReadLine();
        }
        public void RunForFunc()
        {
            FuncTest(ConsoleDisplayOK, ConsoleDisplayFailed);
        }
        public void FuncTest(Action success, Action Fail)
        {
            if (true)
            {
                success();
            }
        }
        public void ConsoleDisplayOK()
        {
            Console.WriteLine("ok.");
        }
        public void ConsoleDisplayFailed ()
        {
            Console.WriteLine("error.");
        }
        static void chineseEat(Person p)
        {
            Console.WriteLine("我是{0},我今年{1}岁了,我吃馒头", p.Name, p.Age);
        }
        Action<String> printReverse =  (string text) =>
        {
            char[] chars = text.ToCharArray();
            Array.Reverse(chars);
            Console.WriteLine(new string(chars));
        };
        static Predicate<int> is666 = (int x) => { return x == 666; };
        static void TestNiMing()
        {
            Func<string, int> returnLength;
            returnLength = text => text.Length;
            //printReverse("hello world");
            List<int> x = new List<int>();
            x.Add(1);
            x.Add(2);
            x.Add(3);
            x.Add(4);
            x.ForEach( (xx) => { Console.WriteLine(xx); });
            bool a = is666(6666);
        }
        public void Run2()
        {
            TestLambdaForFANXING();
            WriteResult(delegate () {
                if (DateTime.Now.Hour <12)
                {
                    return 10;
                }
                else
                {
                    return new Object();
                }
            });
        }
        static void TestLambdaForFANXING()
        {
            Converter<string, int> converter;
            converter = delegate (string s) { return s.Length; };
            PrintConvertedValue("Hellooo", converter);

            PrintConvertedValue("hello", x=>x.Length);
        }
        static void PrintConvertedValue<TInput, TOutput>(TInput input, Converter<TInput, TOutput> converter)
        {
            Console.WriteLine(converter(input));
        }
        delegate T MyFunc<T>();
        static void WriteResult<T>(MyFunc<T> function)
        {
            Console.WriteLine(function());
        }
    }
}
