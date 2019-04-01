using System;
using System.Collections.Generic;
using System.Text;

namespace DennisContainer
{
    /// <summary>
    /// 泛型缓存, 用来取代字典缓存, 因为其性能更高, 也吻合目前的场景.
    /// </summary>
    /// <typeparam name="IT"></typeparam>
    public class DContainerCache<IT>
    {
        private static Type type;
        public static void InitType<T>()
        {
            type = typeof(T);
        }
        public new static Type GetType()
        {
            return type;
        }
    }
}
