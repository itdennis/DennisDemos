using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Demoes.IOCDemos
{
    class BLL_AddStudent
    {
        ISqlHelper sql = null;
        public BLL_AddStudent(ISqlHelper sqlHelper)
        {
            sql = sqlHelper;
        }
        public int AddStudent()
        {
            string str = "";
            return sql.Add();
        }
    }
}
