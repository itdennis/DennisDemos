using System;

namespace BookingSystemIDAL
{
    public interface IOrder
    {
        string ClientName { get; set; }
        string HotelName { get; set; }
        DateTime CheckInDate { get; set; }
        DateTime CheckOutDate { get; set; }
        int OrderRoomCount { get; set; }
    }
}
