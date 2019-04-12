using System;

namespace BookingSystem.BLL
{
    public interface IHotel
    {
        string Name { get; set; }
        int RommCount { get; set; }
        int LeftRoomCount { get; set; }
        bool Check(Order order);
    }
}
