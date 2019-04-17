using System;
using System.Collections.Generic;
using System.Text;

namespace SingletonDemo
{
    /// <summary>
    /// 使用sealed去保证这个类不被派生
    /// 第一次引用类的任何实例的时候创建静态只读的instance
    /// </summary>
    public sealed class StaticSingletonClass
    {
        private static readonly StaticSingletonClass staticSingletonInstance = new StaticSingletonClass();
        private StaticSingletonClass() { }
        public static StaticSingletonClass GetInstance()
        {
            return staticSingletonInstance;
        }
    }
}
