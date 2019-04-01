using System;
using System.Collections.Generic;

namespace DennisContainer
{
    /// <summary>
    /// 构造容器
    /// 依赖注入的三种方式的实现: 构造函数注入, 属性注入, 方法注入
    /// </summary>
    public class DContainer
    {
        private static Dictionary<string, Object> ContainerDictionary = new Dictionary<string, object>();
        public DContainer(){}
        /// <summary>
        /// 注册类型
        /// </summary>
        /// <typeparam name="IT">抽象类型</typeparam>
        /// <typeparam name="T">业务类型</typeparam>
        public void RegisterType<IT, T>()
        {
            ContainerDictionary.Add($"{typeof(IT).FullName}", typeof(T));
            //DContainerCache<IT>.InitType<T>();
        }
        /// <summary>
        /// 生成实例, 根据上面的注册信息来生成实例.
        /// </summary>
        /// <typeparam name="IT"></typeparam>
        /// <returns></returns>
        public IT Reslove<IT>()
        {
            string key = typeof(IT).FullName;

            //Type type = DContainerCache<IT>.GetType();
            Type type = (Type)ContainerDictionary[key];
            System.Reflection.ConstructorInfo[] ctors = type.GetConstructors();
            foreach (var ctor in ctors)
            {
                if (ctor.IsDefined(typeof(DInjectionConstructAttribute), true))
                {
                    var paraArray = ctor.GetParameters();
                    if (paraArray.Length == 0)
                    {
                        return (IT)Activator.CreateInstance(type);
                    }
                    List<object> listParas = new List<object>();
                    foreach (var para in paraArray)
                    {
                        Type paraType = para.ParameterType;
                        string paraKey = paraType.FullName;
                        Type targetParaType = (Type)ContainerDictionary[paraKey];
                        Object oPara = (IT)Activator.CreateInstance(targetParaType);
                        listParas.Add(oPara);
                    }
                    return (IT)Activator.CreateInstance(type, listParas);
                }
            }

            return (IT)Activator.CreateInstance(type);

            //if (ContainerDictionary.ContainsKey(key))
            //{
            //    Type type = (Type)ContainerDictionary[key];
            //    return (IT)Activator.CreateInstance(type);
            //}
            //else
            //{
            //    throw new Exception();
            //}
        }
        
    }
}
