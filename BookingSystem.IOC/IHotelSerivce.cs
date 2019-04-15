using System;

namespace BookingSystem.IOC
{
    public interface IHotelService
    {
        bool CheckOrder(Common.Order order);
    }
}
