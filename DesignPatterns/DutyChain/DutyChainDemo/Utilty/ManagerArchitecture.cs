using System;
using System.Collections.Generic;
using System.Text;

namespace DutyChainDemo
{
    class ManagerArchitecture
    {
        internal static AbstractEmployee SetManager(TitleLevel level)
        {
            AbstractEmployee employee = null;
            switch (level)
            {
                case TitleLevel.worker:
                    employee = new Employee("Leader",TitleLevel.Leader);
                    break;
                case TitleLevel.Leader:
                    employee = new Employee("Manager",TitleLevel.Manager);
                    break;
                case TitleLevel.Manager:
                    employee = new Employee("Boss", TitleLevel.Boss);
                    break;
                case TitleLevel.Boss:
                default:
                    break;
            }
            return employee;
        }
    }
}
