using System;
using BookingSystemIBLL_New;
using BookingSystemBLL;
using DBSchema;

namespace ApplicationView
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initial services.
            IOrderOperationService operationService = new OrderOperationService();
            IHotelOperationService hotelOperationService = new HotelOperationService();

            // get source data
            Console.WriteLine("Get order from somewhere.");
            string orderStr = "";
            
            Order order = operationService.ConvertOrderInput2Model(orderStr);
            var checkResult = hotelOperationService.CheckOrder(order);
        }
    }
}
