using DennisInterface;
using System;

namespace DennisService
{
    public class JWHotel : IHotel
    {
        public string Name { get; set; }
        public int RoomCount { get; set; }
        public int LeftRoomCount { get; set; }
        public JWHotel()
        {
            this.Name = "JW";
            this.RoomCount = 400;
            this.LeftRoomCount = 400;
        }

        public bool Check()
        {
            Console.WriteLine("this is jw hotel for booking check system.");
            return true;
        }
    }
}
