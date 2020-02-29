using AspectCore.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace aspectcore.demo
{
    public class CustomInterceptorAttribute : AbstractInterceptorAttribute
    {
        /// <summary>
        /// 每个被拦截的方法中执行
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            try
            {
                Console.WriteLine("Before service call");
                await next(context); // 执行被拦截的方法
            }
            catch (Exception)
            {
                Console.WriteLine("Service threw an exception");
                throw;
            }
            finally
            {
                Console.WriteLine("After service call");
            }
        }
    }
}
