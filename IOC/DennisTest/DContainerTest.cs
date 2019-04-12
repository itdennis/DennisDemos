using Microsoft.VisualStudio.TestTools.UnitTesting;
using DennisContainer;
using DennisInterface;
using DennisService;

namespace DennisTest
{
    [TestClass]
    public class DContainerTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            {
                IHotel hotel = new JWHotel();

            }

            {
                //DContainer container = new DContainer();
                //container.RegisterType<IHotel, GoldenHotel>();
                //IHotel hotel = container.Reslove<IHotel>();
                ////container.RegisterType<IHotel, GoldenHotel>();
                //hotel.Check();
            }

            //{
            //    DContainer container = new DContainer();
            //    container.RegisterType<IHotel, GoldenHotel>();
            //    IHotel hotel = container.Reslove<IHotel>();
            //    //container.RegisterType<IHotel, GoldenHotel>();
            //    hotel.Check();
            //}
        }
    }
}
