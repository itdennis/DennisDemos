﻿using DennisDemos.Demoes;
using DennisDemos.Demoes.Delegate_Demos;
using DennisDemos.Demoes.WeChat;
using System;
using System.Collections.Generic;

namespace DennisDemos
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                Dictionary<string, string> keyValuePairs = new Dictionary<string, string>()
                {
                    {"a","n" },{ "b","xx"}
                };

                Dictionary<string, string> keyValuePairs2 = new Dictionary<string, string>()
                {
                    {"a","n" },{ "b","xx"}
                };

                List<int> listTest = new List<int> { 1, 2, 31, 1 };

                bool result = true;
                foreach (var item in keyValuePairs)
                {
                    if (keyValuePairs2.ContainsKey(item.Key))
                    {
                        if (keyValuePairs2[item.Key] == item.Value)
                        {
                            continue;
                        }
                        result = false;
                        break;   
                    }
                    result = false;
                    break;
                }
            }




            {
                TraceLogDemo demo = new TraceLogDemo();
                demo.Run();
            }

            {
                ActionDemo demo = new ActionDemo();
                demo.Run();
                Console.ReadKey();
            }
            {
                ThreadDemo demo = new ThreadDemo();
                demo.Run();
            }


            //AreaClass shape = new Shape(100);
            //System.Console.WriteLine(shape.Area);
            {
                JSONDemo demo = new JSONDemo();
                demo.Run();
            }

            {
                string test1 = "a";
                string test2 = "A";
                if (test1.Equals(test2, System.StringComparison.InvariantCultureIgnoreCase))
                {

                }
                if (test1.Equals(test2, System.StringComparison.OrdinalIgnoreCase))
                {

                }
                if (test1.Equals(test2, System.StringComparison.CurrentCultureIgnoreCase))
                {

                }
            }

            {
                StaticTest staticTest = new StaticTest();
                staticTest.Run("1", "2");
                StaticTest staticTest1 = new StaticTest();
                staticTest1.Run("1", "3");
            }

            {
                string base64Encoded = "QzpcVXNlcnNcdi15YW55d3Vcc291cmNlXHJlcG9zXERvYmlcRG9iaVxEb2JpXHd3d3Jvb3RcaW1hZ2VzXElNR184NDIyLnBuZw==";
                string base64Decoded;
                byte[] data = System.Convert.FromBase64String(base64Encoded);
                base64Decoded = System.Text.ASCIIEncoding.ASCII.GetString(data);
            }


            {
                string base64Decoded = @"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";
                string base64Encoded;
                byte[] data = System.IO.File.ReadAllBytes(base64Decoded);
                base64Encoded = System.Convert.ToBase64String(data);
            }
        }
    }
}
