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
            using (StreamReader sr = File.OpenText("Demoes\\JsonDemo\\json1.json"))
            {
                string jsonStr = sr.ReadToEnd();

                var res = nameof(Run);

                Dictionary<string, bool> keyValuePairs = new Dictionary<string, bool>();

                keyValuePairs.Add("sleeve strategy code", true);
                keyValuePairs.Add("sleeve strategy name", true);



                List<string> keys = new List<string>(keyValuePairs.Keys);
                foreach (string key in keys)
                {
                    keyValuePairs[key] = false;
                }
                    







                Console.ReadKey();
                //JArray array = JArray.Parse(jsonStr);

                //foreach (JObject content in array.Children<JObject>())
                //{
                //    foreach (JProperty prop in content.Properties())
                //    {
                //        Console.WriteLine(prop.Name);
                //    }
                //}






                JObject jObject = JObject.Parse(jsonStr);

                




                JToken CatInformation = jObject["CatInformation"];

                JToken cat_Id = CatInformation["Id"];

                Console.WriteLine($"Got cat id is: {cat_Id.ToString()}");

                JToken Activities = jObject["Activities"];

                JToken swim_Id = Activities[0]["Id"];

                JArray jArray_Activities = (JArray)Activities;


                JArray metadata = (JArray)jObject["CatInformation"];
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

        private void GetKeys(List<string> keys, JToken token)
        {
            if (token.Type == JTokenType.Object)
            {
                foreach (var item in token)
                {
                    GetKeys(keys, item);
                }
            }
            if (token.Type == JTokenType.Property)
            {
                
            }
            //foreach (var item in jObject.Properties())
            //{
            //    keys.Add(item.Name);
            //    var value = jObject[item.Name];
            //    if (value.HasValues)
            //    {
            //        if (value.Type != JTokenType.Array)
            //        {
            //            GetKeys(keys, jObject[item.Name].ToObject<JObject>());
            //        }
            //        else
            //        {
            //            foreach (var itemInArray in value)
            //            {
            //                itemInArray
            //            }
            //        }
            //    }
            //}
        }


    }
    public class Student
    {
        public string Name { get; set; }

        public int y { get; set; }

    }
}
