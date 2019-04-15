using System;
using System.Collections.Generic;
using System.Text;

namespace BookingSystem.IOC
{
    public interface IOrderService
    {
        Common.Order OrderConverter(string orderStr);
    }
}
