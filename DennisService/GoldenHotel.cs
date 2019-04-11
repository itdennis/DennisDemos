using DennisContainer;
using System;
using System.Collections.Generic;
using System.Text;

namespace DennisService
{
    /// <summary>
    /// 实际注册的hotel类
    /// </summary>
    public class GoldenHotel
    {
        public string Name { get; set; }
        public int RoomCount { get; set; }
        public int LeftRoomCount { get; set; }
        public bool NeedPassPort { get; set; }
        [DInjectionConstruct]
        public GoldenHotel(bool needPassport)
        {
            this.Name = "Golden";
            this.RoomCount = 400;
            this.LeftRoomCount = 400;
            this.NeedPassPort = needPassport;
        }

        public bool Check()
        {
            Console.WriteLine("this is golden hotel for booking check system.");
            return true;
        }
    }
}
