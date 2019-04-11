using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryDemo
{
    public interface IFactory
    {
        Operation CreateOperation();
    }
}
