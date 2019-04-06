using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryDemo
{
    public class VolunteerFactory : ILeiFengFactory
    {
        public LeiFeng CreateLeiFeng()
        {
            return new Volunteer();
        }
    }
}
