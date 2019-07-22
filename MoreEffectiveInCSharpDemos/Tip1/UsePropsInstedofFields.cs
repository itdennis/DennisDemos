using System;
using System.Collections.Generic;
using System.Text;

namespace MoreEffectiveInCSharpDemos.Tip1
{
    /// <summary>
    /// 隐式属性来表示可变的数据
    /// 此时编译器会自动补全一个`后援字段`, 在我们自己写的类中想操作这个后援字段也必须通过隐式属性
    /// 属性可以创建为索引器
    /// </summary>
    public class UsePropsInstedofFields
    {
        //隐式属性
        public int Name { get; set; }
        //属性可以进行权限设置
        public int Age { get; private set; }
    }

    public class BaseType
    {
        private object syncHandle = new object();
        /// <summary>
        /// 属性具备方法的一切优势, 包括可以声明为virtual和abstract
        /// 属性可以进行多线程的安全处理
        /// </summary>
        public virtual string Name
        {
            get
            {
                lock (syncHandle)
                {
                    return Name;
                }
            }
            protected set
            {
                //属性可以进行错误输入验证
                if (!string.IsNullOrEmpty(value))
                {
                    lock (syncHandle)
                    {
                        Name = value;
                    }
                }
                else
                {
                    throw new ArgumentException("Name cannot be blank", nameof(Name));
                }
            }
        }
    }
    /// <summary>
    /// 子类的修改不会破坏二进制层面的兼容性
    /// 数据的验证逻辑只需要写在一个地方就可以了.
    /// </summary>
    public class DerivedType : BaseType
    {
        public override string Name { get => base.Name; protected set => base.Name = value; }
    }
}
