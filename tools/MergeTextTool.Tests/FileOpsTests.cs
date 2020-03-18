using MergeTextTool.src;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MergeTextTool.Tests
{
    /// <summary>
    /// 1. success merge heads (in-order)
    /// 2. success merge heads (not in-order)
    /// 3. success merge rows (in-order)
    /// 4. success merge rows (not in-order)
    /// </summary>
    [TestClass]
    public class FileOpsTests
    {
        [TestMethod]
        public void GetFileHead_Success()
        {
            // arrange
            string filesPath = "./tests/testdata/a.txt";
            IDataSourceOps dataSourceOps = new FileOps();

            // act
            var head = dataSourceOps.GetHead(filesPath);

            // assert
            Assert.IsNotNull(head);
        }

        [TestMethod]
        public void MergeFileHead_Success() 
        {
            // arrange
            string fileA = "./tests/testdata/a.txt";
            string fileB = "./tests/testdata/b.txt";
            IDataSourceOps ops = new FileOps();
            HashSet<string> mergedHead = null;

            // act
            var head_A = ops.GetHead(fileA);
            var head_B = ops.GetHead(fileB);
            mergedHead = ops.MergeHead(new List<string>() { head_A, head_B });

            // assert
            Assert.IsNotNull(mergedHead);
            Assert.IsTrue(mergedHead.Count > 0);
        }

        [TestMethod]
        public void GetFileContent_Success() 
        {
            GetFileContent_Success_Async().GetAwaiter().GetResult();
            Console.ReadKey();
        }

        private async Task GetFileContent_Success_Async() 
        {
            string fileA = "./tests/testdata/a.txt";
            IDataSourceOps ops = new FileOps();

            var headA = ops.GetHead(fileA);
            await foreach (var line in ops.GetContentAsync(fileA))
            {
                Console.WriteLine(line);
            }
        }

        [TestMethod]
        public void CreateNewFile_Success() 
        {
            // arrange
            string fileA = "./tests/testdata/a.txt";
            string fileB = "./tests/testdata/b.txt";
            string resultFilePath = "./tests/output/result.txt";
            string outputDir = "./tests/output/";

            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }
            IDataSourceOps ops = new FileOps();
            HashSet<string> mergedHead = null;

            // act
            if (File.Exists(resultFilePath))
            {
                File.Delete(resultFilePath);
            }

            var head_A = ops.GetHead(fileA);
            var head_B = ops.GetHead(fileB);
            mergedHead = ops.MergeHead(new List<string>() { head_A, head_B });
            ops.CreateNewFile(resultFilePath, mergedHead);

            // assert
            Assert.IsTrue(File.Exists(resultFilePath));
        }
    }
}
