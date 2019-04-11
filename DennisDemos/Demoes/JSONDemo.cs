using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Demos
{
    class JSONDemo : DemoBase
    {
        public override void Run()
        {
            List<Student> ls = new List<Student>();
            Student one = new Student(){ Name = "武松", y = 250 };
            Student one2 = new Student() { Name = "武松2", y = 350 };
            Student one3 = new Student() { Name = "武松3", y = 450 };

            ls.Add(one);
            ls.Add(one2);
            ls.Add(one3);
            //序列化
            string jsonData = JsonConvert.SerializeObject(ls);
        }
    }
    public class Student
    {
        public string Name { get; set; }

        public int y { get; set; }

    }
}
