using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1_demo.Extension
{
    public static class ActionExtension
    {
        public static void Execute(this Action action)
        {
            if (action != null)
            {
                action();
            }
        }

        public static void Execute<T>(this Action<T> action, T obj)
        {
            if (action != null)
            {
                action(obj);
            }
        }

        public static T Execute<T>(this Func<T> func)
        {
            if (func != null)
            {
                return func();
            }
            return default(T);
        }

        [SuppressMessage("Microsoft.Naming", "CA1715:Identifiers should have correct prefix", Justification = "For prefix generic type parameter name 'Parameter' with 'T'.")]
        public static TResult Execute<TResult, T>(this Func<T, TResult> func, T args)
        {
            if (func != null)
            {
                return func(args);
            }
            return default(TResult);
        }
    }
}
