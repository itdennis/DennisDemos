using DennisDemos.Demoes;
using DennisDemos.Demoes.WeChat;

namespace DennisDemos
{
    class Program
    {
        static void Main(string[] args)
        {
            OtherDemo otherDemo = new OtherDemo();
            otherDemo.Run();
            //var wx = new WeChat();
            //var data = new
            //{
            //    first = new { value = "1" },
            //    keyword1 = new { value = "2" },
            //    keyword2 = new { value = "3" },
            //    keyword3 = new { value = "4" },
            //    keyword4 = new { value = "5" },
            //    remark = new { value = "remark" }

            //};

            //wx.SendTemplete("2l8wR0HMUXJkVldvgcSaFFmUCVgePhZ7dKzNDSbp8NA", "__openId__", data);


            //ConfigureAwaitDemo configureAwaitDemo = new ConfigureAwaitDemo();
            //configureAwaitDemo.Run().Wait() ;


            //DemoBase runer;

            //runer = new CheckWeekday();

            //runer.Run();
            //MainAsyncTest.Run().Wait();
            //AsyncDemo asyncDemo = new AsyncDemo();
            //asyncDemo.Run();
            //JSONDemo jSONDemo = new JSONDemo();
            //jSONDemo.Run();
        }
    }
}
