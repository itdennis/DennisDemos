using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Demos
{
    /// <summary>
    /// 使用泛型实现缓存：
    /// 区别于之前用字典或静态变量存储缓存，这里不同类型的“T”会被即时编译为不同的类型从而实现缓存
    /// </summary>
    /// <typeparam name="T">缓存的数据类型 例如用户信息表实体：UserInfo</typeparam>
    public static class GenericCacheDemo<T> where T : class
    {
        /// <summary>
        /// 版本：可以从数据库的表或配置文件中读取，每次取值时查询是否更新缓存
        /// </summary>
        private static string _version = string.Empty;
        /// <summary>
        /// 缓存：缓存的内容
        /// </summary>
        private static List<T> _genericCache = null;
        /// <summary>
        /// 错误信息：保存缓存数据失败时的错误信息
        /// </summary>
        private static string _errMsg = string.Empty;
        /// <summary>
        /// 数据库：该参数表所在的数据库名
        /// </summary>
        private static string _database = string.Empty;
        /// <summary>
        /// 错误信息：保存缓存数据失败时的错误信息
        /// </summary>
        public static string ErrMsg { get => _errMsg; set => _errMsg = value; }

        /// <summary>
        /// 读取缓存
        /// </summary>
        private static void ReadData()
        {
            if (string.IsNullOrEmpty(_database))
            {
                //从数据库或配置文件中对该类型所在数据库进行赋值
                _database = ConfigurationManager.AppSettings.Get(typeof(T).Name);
            }
            //从数据库中读取数据并赋值给缓存
            //DataSet dataSet = DBHelper.SqlHelper(string.Format("select * from {0} where 1=1", typeof(T).Name), _database, ref _errMsg);
            //if (dataSet != null && dataSet.Tables.Count > 0)
            //{
            //    _genericCache = MapEntityHelper.MapEntityList<T>(dataSet.Tables[0]).ToList();
            //}
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <returns>缓存内容</returns>
        public static List<T> GetCache()
        {
            //判断缓存内容是否需要更新 缓存版本可以存储在数据库中或配置文件中
            if (_genericCache == null || ConfigurationManager.AppSettings.Get(typeof(T).Name + ".Version") != _version)
            {
                ReadData();
            }
            return _genericCache;
        }
    }
}
