
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Demos.IOCDemos
{
    public class UI
    {
        /// <summary>
        /// UI对于BLL来说是依赖注入, BLL对于UI来说就是控制反转.
        /// </summary>
        public UI()
        {
            ISqlHelper sqlHelper = new DALMSSqlHelper();//初始化dal
            BLL_AddStudent s = new BLL_AddStudent(sqlHelper);//业务逻辑BLL部分
            s.AddStudent();
        }
        
    }
}
