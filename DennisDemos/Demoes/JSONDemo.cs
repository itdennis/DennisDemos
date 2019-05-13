using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Demoes
{
    class JSONDemo : DemoBase
    {
        public override void Run()
        {
            string a = "666";
            string b = "777";
            string testParameterList = string.Format(@"{{""key"": ""{0}"",'key2':'{1}'}}", a, b);

            string test4others = @"{
                                        ""filepath"": ""sdfssfdsfsdfsdf"",
                                        ""Hyperlinklist"" : [
                                                {""a"":""aValue"", ""b"":""1231placeholder4444"", ""c"":""xxx""},
                                                {""a"":""aValue"", ""b"":""placeholder"", ""c"":""xxx""},
                                                {""a"":""aValue"", ""b"":""placeholder"", ""c"":""xxx""}
                                            ]}";


            string placeholder = "99999999999";
            string xxx = "44444444444444444444";

            string result = string.Format(@"{{
                                        ""filepath"": ""sdfssfdsfsdfsdf"",
                                        ""Hyperlinklist"" : [
                                                {{""a"":""aValue"", ""b"":""1231{0}4444"", ""c"":""{1}""}},
                                                {{""a"":""aValue"", ""b"":""placeholder"", ""c"":""xxx""}},
                                                {{""a"":""aValue"", ""b"":""placeholder"", ""c"":""xxx""}}
                                            ]}}", placeholder, xxx );

            
            string result2 = $@"{{
                                        ""filepath"": ""sdfssfdsfsdfsdf"",
                                        ""Hyperlinklist"" : [
                                                {{""a"":""aValue"", ""b"":""1231{placeholder}4444"", ""c"":""{xxx}""}},
                                                {{""a"":""aValue"", ""b"":""placeholder"", ""c"":""xxx""}},
                                                {{""a"":""aValue"", ""b"":""placeholder"", ""c"":""xxx""}}
                                            ]}}";

            var jObjectr = JsonConvert.DeserializeObject(result2);


            string testParameterList2 = string.Format("{0}", a);


            string ss = $"{a}, this is dynimic string";



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
