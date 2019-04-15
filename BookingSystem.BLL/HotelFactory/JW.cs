using System;
using System.Collections.Generic;
using System.Text;

namespace BookingSystem.BLL
{
    public class JW : IHotel
    {
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int RommCount { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int LeftRoomCount { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool Check { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        bool IHotel.Check(Common.Order order)
        {
            throw new NotImplementedException();
        }
    }
}
