using System;
using System.Collections.Generic;
using System.Text;

namespace BookingSystem.BLL
{
    public class HotelService
    {
        public bool CheckOrder(Order order)
        {
            HotelFactory hotelFactory = new HotelFactory();
            if (order.HotelName == "JW")
            {
                return hotelFactory.CreateHotel(HotelType.JW).Check(order);
            }
            else if (order.HotelName == "Golden")
            {
                return hotelFactory.CreateHotel(HotelType.Golden).Check(order);
            }
            throw new Exception($"[{this.GetType().Name}] No this hotel type : {order.HotelName}");
        }
    }
}
