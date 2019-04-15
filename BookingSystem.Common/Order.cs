using System;

namespace BookingSystem.Common
{
    public class Order
    {
        public string HotelName { get; set; }
        public string ClientName { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int OrderRoomCount { get; set; }
    }
}
