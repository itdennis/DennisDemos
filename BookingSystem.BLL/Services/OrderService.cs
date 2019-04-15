using System;
using System.Collections.Generic;
using System.Text;

namespace BookingSystem.BLL
{
    public class OrderService : IOC.IOrderService
    {
        public Common.Order OrderConverter(string orderStr)
        {
            Common.Order order = new Common.Order
            {

            };
            return order;
        }
    }
}
