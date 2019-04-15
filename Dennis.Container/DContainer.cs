using System;
using System.Collections.Generic;

namespace Dennis.Container
{
    public class DContainer
    {
        private static Dictionary<string, Object> ContainerCache = new Dictionary<string, object>();

        /// <summary>
        /// 注册对象
        /// </summary>
        /// <typeparam name="IT">抽象类型</typeparam>
        /// <typeparam name="T">实际类型</typeparam>
        public void Register<IT, T>()
        {
            ContainerCache.Add($"{typeof(IT).FullName}", typeof(T));
        }

        /// <summary>
        /// 生成实例.
        /// </summary>
        /// <typeparam name="IT"></typeparam>
        /// <returns></returns>
        public IT Resolve<IT>()
        {
            string key = typeof(IT).FullName;
            if (ContainerCache.ContainsKey(key))
            {
                Type type = (Type)ContainerCache[key];
                return (IT)Activator.CreateInstance(type);
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
