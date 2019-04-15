using System;

namespace BookingSystem.Run
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().RunBookingSystem();
        }

        private void RunBookingSystem()
        {
            IOC.IHotelService hotelService = new BLL.HotelService();
            IOC.IOrderService orderService = new BLL.OrderService();
            UI.RunUI run = new UI.RunUI();
            run.HotelService = hotelService;
            run.OrderService = orderService;
            run.Run();
        }
    }
}
