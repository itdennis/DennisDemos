using System;
using System.Collections.Generic;
using System.Text;

namespace DutyChainDemo
{
    /// <summary>
    /// 抽象员工类的信息
    /// </summary>
    abstract class AbstractEmployee
    {
        public abstract string Name { get; set; }
        public abstract AbstractEmployee Manager { get; set; }
        public abstract TitleLevel Level { get; set; }
        public abstract void CheckRequest(Request request);
    }
}
