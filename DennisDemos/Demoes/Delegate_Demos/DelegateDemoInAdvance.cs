using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Demoes
{
    public static class DelegateDemoInAdvance
    {
        /// <summary>
        /// 对于日志类的异常封装.
        /// </summary>
        /// <param name="action"></param>
        public static void SafeInvoke(this Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static void SafeSaveChange(this Func<string> func)
        {
            var res = func.Invoke();
            if (true)
            {
                Console.WriteLine(res);
            }
        }

        /// <summary>
        /// 封装的ADO.net 的数据库访问类.git不好使了...
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static T Excute<T>(string sql, Func<SqlCommand, T> func)
        {
            new Func<string>(() => { return "1"; }).SafeSaveChange();


            using (SqlConnection connection = new SqlConnection(""))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                T t = default(T);
                new Action(() =>
                {
                    SqlCommand command = new SqlCommand();
                    t = func(command);
                    transaction.Commit();
                }).SafeInvoke();
                return t;
            }
        }
    }
}
