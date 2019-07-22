using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Demoes
{
    class JSONDemo : DemoBase
    {
        public override void Run()
        {




            using (StreamReader sr = File.OpenText(@"C:\Users\v-yanywu\Documents\WeChat Files\it_dennis\FileStorage\File\2019-07\data.json"))
            {
                var jsonStr = sr.ReadToEnd();
                var jObject = JObject.Parse(jsonStr);
                JArray metadata = (JArray)jObject["Metadata"];
                // 由于得到的 metadata 是JArray, 属于Array, 那么访问Array中元素的方式有两种:
                // 1. 直接使用index访问指定元素
                var fields1 = metadata[0]["Fields"];

                // 2. 遍历数组, 得到元素
                foreach (JObject item in metadata)
                {
                    JToken fields2 = item.GetValue("Fields");
                }
            }
        }

    }
    public class Student
    {
        public string Name { get; set; }

        public int y { get; set; }

    }
}
