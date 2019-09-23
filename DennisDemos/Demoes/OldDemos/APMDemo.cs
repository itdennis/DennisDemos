using System.Diagnostics;
using System.Net;
using System.Threading;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace DennisDemos.Demoes
{
    /// <summary>
    /// APM异步编程模型, Asynchronous Programming Model
    /// </summary>
    class APMDemo
    {
        public void RunAPMDemo()
        {
            Debug.WriteLine("current thread id: {0}", Thread.CurrentThread.ManagedThreadId);
            var request = WebRequest.Create("https://github.com/");
            request.BeginGetResponse(new AsyncCallback(t =>
            {
                var response = request.EndGetResponse(t);
                var stream = response.GetResponseStream();
                using (StreamReader reader = new StreamReader(stream))
                {
                    StringBuilder sb = new StringBuilder();
                    while (!reader.EndOfStream)
                    {
                        var content = reader.ReadLine();
                        sb.Append(content);
                    }
                    Debug.WriteLine("【Debug】" + sb.ToString().Trim().Substring(0, 100) + "...");//只取返回内容的前100个字符 
                    Debug.WriteLine("【Debug】异步线程ID:" + Thread.CurrentThread.ManagedThreadId);
                }
            }), null);
            Debug.WriteLine("【Debug】主线程ID:" + Thread.CurrentThread.ManagedThreadId);
        }
        /// <summary>
        /// 贸然使用result会造成死锁问题, 因为result是同步的, 而await是异步的. 
        /// </summary>
        public void testAwait1()
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            //var result = GetUlrString("https://github.com/").Result;
            GetUlrString("https://github.com/").Wait();
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            Console.ReadKey();
        }
        public async static Task<string> GetUlrString(string url)
        {
            using (HttpClient http =new HttpClient())
            {
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                return await http.GetStringAsync(url);
            }
        }
    }
}
