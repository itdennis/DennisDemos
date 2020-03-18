using MergeTextTool.src;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MergeTextTool
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult() ;
            Console.ReadKey();
        }

        static async Task MainAsync(string[] args)
        {
            #region Prepare data

            // will change the hard code into config in the future.
            string fileA = "./testdata/a.txt";
            string fileB = "./testdata/b.txt";
            string resultFilePath = "./testdata/output/result.txt";
            string outputDir = "./testdata/output/";
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }
            if (File.Exists(resultFilePath))
            {
                File.Delete(resultFilePath);
            } 
            #endregion

            IDataSourceOps ops = new FileOps();

            var head_A = ops.GetHead(fileA);
            var head_B = ops.GetHead(fileB);

            HashSet<string> mergedHead = ops.MergeHead(new List<string>() { head_A, head_B });
            ops.CreateNewFile(resultFilePath, mergedHead);

            var head_A_List = head_A.Split("\t").ToList();
            var head_B_List = head_B.Split("\t").ToList();

            await foreach (var line in ops.GetContentAsync(fileA))
            {
                if (head_A == line)
                {
                    continue;
                }
                Console.WriteLine($"read line from file : {fileA}, content: {line}");
                string newLine = ops.ConvertToTargetFormat(head_A_List, mergedHead, line);
                ops.AppendNewLineToFile(resultFilePath, newLine);
            }


            await foreach (var line in ops.GetContentAsync(fileB))
            {
                if (head_B == line)
                {
                    continue;
                }
                Console.WriteLine($"read line from file : {fileB}, content: {line}");
                string newLine = ops.ConvertToTargetFormat(head_B_List, mergedHead, line);
                ops.AppendNewLineToFile(resultFilePath, newLine);
            }

        }
    }
}
