using System;
using System.Collections.Generic;
using System.Text;

namespace BookingSystem.BLL
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
