using System;
using BookingSystem.BLL;

namespace BookingSystem.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string orderStr = "";
                var order = new OrderService().OrderConverter(orderStr);

                var result = new HotelService().CheckOrder(order);

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
    }
}
