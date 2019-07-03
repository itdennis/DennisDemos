using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos
{
    public abstract class AreaClass
    {
        protected AreaClass()
        {
            Area = ClocArea();
        }
        public int Area { get; set; }
        public virtual int ClocArea()
        {
            return 0;
        }
    }

    public class Shape : AreaClass
    {
        private readonly int _index;
        public Shape(int index):base()
        {
            _index = index;
        }
        public override int ClocArea()
        {
            return _index * _index;
        }
    }
}
