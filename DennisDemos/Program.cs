using DennisDemos.Demoes;

namespace DennisDemos
{
    class Program
    {
        static void Main(string[] args)
        {

            ConfigureAwaitDemo configureAwaitDemo = new ConfigureAwaitDemo();
            configureAwaitDemo.Run().Wait() ;


            DemoBase runer;

            runer = new CheckWeekday();

            runer.Run();
            //MainAsyncTest.Run().Wait();
            //AsyncDemo asyncDemo = new AsyncDemo();
            //asyncDemo.Run();
            //JSONDemo jSONDemo = new JSONDemo();
            //jSONDemo.Run();
        }
    }
}
