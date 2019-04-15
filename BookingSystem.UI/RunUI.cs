using System;
using System.Collections.Generic;
using System.Text;

namespace BookingSystem.UI
{
    public class RunUI
    {
        //属性注入
        public IOC.IHotelService HotelService { get; set; }
        public IOC.IOrderService OrderService { get; set; }

        //构造函数注入
        //public RunUI(IOC.IHotelService hotelService)
        //{
        //}
        //接口注入
        public void Inject(IOC.IHotelService hotelService)
        {

        }

        public void Run()
        {
            try
            {
                string orderStr = "";
                Common.Order order = OrderService.OrderConverter(orderStr);

                var result = HotelService.CheckOrder(order);

                if (result)
                {
                    Console.WriteLine("order is valied.");
                }
                else
                {
                    Console.WriteLine("order is invalied.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.ReadKey();
        }

        public void RunWithDContainer()
        {
            //Dennis.Container.DContainer container = new Dennis.Container.DContainer();

            //container.Register<IOC.IOrderService, BLL.OrderService>();
            //var orderService = container.Resolve<IOC.IOrderService>();


            //container.Register<IOC.IHotelService, BLL.HotelService>();
            //var hotelService = container.Resolve<IOC.IHotelService>();
        }
    }
}
