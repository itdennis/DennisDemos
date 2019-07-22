using System;
using System.Collections.Generic;
using System.Text;

namespace MoreEffectiveInCSharpDemos.Tip3
{
    /// <summary>
    /// 尽量把值类型设计成为不可变的类型
    /// 原子类型, 与值类型, 设计成为不可变的类型.
    /// </summary>
    public class UseValueDataAsNotChangedData
    {
    }
    /// <summary>
    /// 错误的示例, 将我们熟知的 Address 设计的可变的影响
    /// </summary>
    public struct Address
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }

        public string State { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }
    }

    /// <summary>
    /// 将setter去掉, 为了能够使用AddressNotChangable, 就必须提供一个构造函数
    /// </summary>
    public struct AddressNotChangable
    {
        public string Line1 { get; }
        public string Line2 { get; }

        public string State { get; }
        public int ZipCode { get; }
        public string City { get; }

        public AddressNotChangable(string line1, string line2, string state, string city, int zipCode)
        : this()
        {
            Line1 = line1;
            Line2 = Line2;
            State = state;
            City = city;
            ZipCode = zipCode;
        }
    }

    /// <summary>
    /// 创建不可变类型时, 要注意代码中是否有存在的漏洞导致可以改变该对象的内部状态.
    /// 值类型由于没有派生类, 因此无需方法通过派生类来修改基类内容的做法.
    /// 但是如果某个不可变类型的某个字段引用了某个可变类型的对象, 那就要小心了.
    /// </summary>
    public struct PhoneList
    {
        private readonly string[] phones;
        public PhoneList(string[] ph) { phones = ph; }
        public IEnumerable<string> Phones { get { return phones; } }
    }


    public class ExampleUsage
    {
        /// <summary>
        /// 显然这个例子说明了, 如果当前code放在一个上下文为多线程的环境中, 有可能会引发问题.因为有可能一个线程在修改了一个或者两个属性后, 
        /// 另一个线程查看属性时会引起歧义.
        /// 就算不是放在多线程的上下文中也会有问题, 比如程序在给一个属性赋值之后突然抛出异常, 而导致a1处于无效的状态.
        /// 通常想要避免上述问题, 就要添加很多验证代码来保证原子性.
        /// 那么怎么做能够更加完美的解决这些问题呢? 
        /// 就是将 Address设计成为不可变的结构体.
        /// </summary>
        public void Show()
        {
            Address a1 = new Address();
            a1.Line1 = "wuyanyu";
            a1.Line2 = "microsoft";
            a1.City = "北京市";
            a1.State = "北京";
            a1.ZipCode = 00001;

            // Modify
            a1.City = "大连"; // 实际上当修改 city为大连时, state还是北京, zipcode也是00001, 这时city和state以及zipcode是无法匹配的. 
            a1.State = "liaoning"; // 当修改到state为liaoning后, city和state是匹配的. 但是zipcode还没有
            a1.ZipCode = 550023; // 都匹配了. 
        }

        /// <summary>
        /// 好处:
        /// address只有两种状态: 修改之前, 和修改之后
        /// address的地址指向要么是原来的地址, 要么是新地址
        /// 即便是修改时构造发生问题, address也是指向旧地址, 不会无效.
        /// </summary>
        public void ShowNotChangableExample()
        {
            AddressNotChangable address = new AddressNotChangable("11", "2", "3", "4", 5);
            //to change
            address = new AddressNotChangable("a", "a", "a", "a",4);
        }

        public void ShowPhoneList()
        {
            String[] phones = new string[10];
            //初始化
            PhoneList phoneList = new PhoneList(phones);

            //修改
            phones[5] = "huawei";
        }
    }
}
