using System;

namespace DennisInterface
{
    public interface IHotel
    {
        string Name { get; set; }
        int RoomCount { get; set; }
        int LeftRoomCount { get; set; }
        bool Check();
    }
}
