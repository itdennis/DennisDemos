using System;
using System.Reflection;

namespace DennisDemos.Demoes
{
    public class ReflectDemo
    {
        /// <summary>
        /// 反射的作用就是使程序具备动态获取信息, 调用方法的能力.
        /// </summary>
        public void Run()
        {
            Type T1 = typeof(TClass);
            //动态调用方法demo
            T1.InvokeMember("fun", BindingFlags.InvokeMethod, null, new TClass(), new String[] { "123" });
            T1.GetMethod("fun", BindingFlags.Instance | BindingFlags.Public).Invoke(new TClass(), new string[] { "testfun1" });

            //动态构造函数
            Assembly asm = Assembly.GetExecutingAssembly();
            TClass obj1 = (TClass)asm.CreateInstance("net.tclass", true);


            //获取属性
            var obj = new TClass();
            obj.Name = "张三";
            Type type = typeof(TClass);
            //获取属性
            var Name = type.InvokeMember("Name", BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance, null,
                                 obj, new object[] { }) as string;
            Console.WriteLine(obj.Name);
            //设置属性
            type.InvokeMember("Name", BindingFlags.SetProperty | BindingFlags.Public | BindingFlags.Instance, null,
                                  obj, new object[] { "新属性（李四）" });
            Console.WriteLine(obj.Name);

            //=====================

            PropertyInfo[] pros = type.GetProperties();//
            PropertyInfo pro = pros[0];

            var value = pro.GetValue(type);//获取值
            Console.ReadKey();
        }
    }
    public class TClass
    {
        public string name;
        public string Name { get; set; }
        public TClass()
        {
            Console.WriteLine("构造函数被执行了。。");
        }
        public TClass(string str)
        {
            Console.WriteLine("有参构造函数被执行了。。" + str);
        }
        public void fun(string str)
        {
            Console.WriteLine("我是fun方法，我被调用了。" + str);
        }
        public void fun2()
        {
            Console.WriteLine("我是fun2方法，我被调用了。");
        }

        public static void fun3()
        {
            Console.WriteLine("我是fun3静态方法,我被调用了");
        }
    }
}
