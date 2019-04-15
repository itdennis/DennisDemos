using System;
using System.Collections.Generic;
using System.Text;

namespace BookingSystem.BLL
{
    public class HotelFactory
    {
        private IHotel hotel;
        public IHotel CreateHotel(HotelType hotelType)
        {
            switch (hotelType)
            {
                case HotelType.JW:
                    hotel = new JW();
                    break;
                case HotelType.Golden:
                    hotel = new Golden();
                    break;
                default:
                    throw new Exception($"[{this.GetType().Name}] No this hotel type.");
            }
            return hotel;
        }
    }
}
