using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using System.Net;
using System.Diagnostics;
using System.IO;
using System.Collections;

namespace DennisDemos.Demos
{
    class AsyncDemo
    {
        public void Run()
        {
            //var result = Test1();
            //Console.WriteLine(result.Result);// 必须等到结果之后才能继续执行下面的代码. 所以result是同步的.
            //Console.WriteLine("test1 end");
            Console.WriteLine("MainAsync start.");
            var resultTest = FetchURLAsync(new List<string>() { "www.baidu.com"});

            var result2 = MainAsync();
            
            Console.WriteLine("MainAsync end.");
            while (!result2.IsCompleted)
            {
                Thread.Sleep(1000);
                Console.WriteLine("waiting..");
            }
            Console.WriteLine("end.");
        }
        #region MainAsync
        static async Task MainAsync()
        {
            Task<string> task = ReadFileAsync("LogicDocument_DocAveLoadBalance.docx");   //❶ 开始异步读取
            Task<string[]> fetchURL = FetchURLAsync(new List<string> { "www.baidu.com" });
            try
            {
                Console.WriteLine("start to wait reads file reult.");
                string text = await task;     //❷ 等待内容 这里的await执行的异步操作需要上一个await返回出结果, 所以这里需要等待, 而不是直接返回.
                Thread.Sleep(10000);
                Console.WriteLine("File contents");
            }
            catch (IOException e)    //❸ 处理IO失败
            {
                Console.WriteLine("Caught IOException: {0}", e.Message);
            }
        }

        static async Task<string> ReadFileAsync(string filename)
        {
            Console.WriteLine("start to read file async.");
            using (var reader = File.OpenText(filename))    //❹　同步打开文件
            {
                var result = await reader.ReadToEndAsync().ConfigureAwait(true);
                Thread.Sleep(1000);
                Console.WriteLine("get read file async result.");
                return result;
            }
        }
        #endregion
        /// <summary>
        /// demo in C# in deep
        /// </summary>
        /// <param name="urls"></param>
        /// <returns></returns>
        static async Task<string[]> FetchURLAsync(List<string> urls)
        {
            // TODO：验证是否获取到了URL
            var tasks = urls.Select(async url =>
            {
                using (var client = new HttpClient())
                {
                    return await client.GetStringAsync(url);
                }
            }).ToList();
            string[] resutls = await Task.WhenAll(tasks);
            return resutls;
        }
    }
}
