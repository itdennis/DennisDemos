using System;

namespace DennisInterface
{
    /// <summary>
    /// 将注册到系统的Hotel抽象出IHotel基类
    /// 以后再注册的Hotel都需要实现这个基类的内容
    /// </summary>
    public interface IHotel
    {
        string Name { get; set; }
        int RoomCount { get; set; }
        int LeftRoomCount { get; set; }
        bool Check();
    }
}
