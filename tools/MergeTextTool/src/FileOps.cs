using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MergeTextTool.src
{
    public class FileOps : IDataSourceOps
    {
        public string GetHead(string filePath)
        {
            Validate(filePath);

            StreamReader file = new StreamReader(filePath);
            string head;
            while ((head = file.ReadLine()) != null)
            {
                System.Console.WriteLine($"success get file: {filePath} head: {head}");
                break;
            }

            if (string.IsNullOrEmpty(head))
            {
                throw new Exception($"cannot read head from file : {filePath}");
            }
            else
            {
                return head;
            }
        }

        public HashSet<string> MergeHead(List<string> heads)
        {
            HashSet<string> mergedHeads = new HashSet<string>();
            foreach (var head in heads)
            {
                foreach (var item in head.Split("\t").ToList())
                {
                    mergedHeads.Add(item);
                }
            }
            return mergedHeads;
        }

        public async IAsyncEnumerable<string> GetContentAsync(string filePath) 
        {
            Validate(filePath);
            using (StreamReader file = new StreamReader(filePath))
            {
                string line = null;
                while ((line = await file.ReadLineAsync()) != null)
                {
                    yield return line;
                }
            }
        }

        public void CreateNewFile(string filePath, HashSet<string> newHeads) 
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new Exception($"filePath should not be empty.");
            }

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            using (StreamWriter sw = new StreamWriter(filePath))
            {
                var head = string.Join("\t", newHeads);
                sw.WriteLine(head) ;
            }
        }

        public string ConvertToTargetFormat(List<string> sourceHead, HashSet<string> targetHead, string sourceLine) 
        {
            if (string.IsNullOrEmpty(sourceLine) || sourceHead == null || targetHead == null)
            {
                throw new Exception($"Invalid data parameters.");
            }

            if (sourceHead.Count > targetHead.Count)
            {
                throw new Exception($"Invalid data: source head count should not bigger than target head count.");
            }

            List<string> src = sourceLine.Split("\t").ToList();
            if (sourceHead.Count != src.Count)
            {
                throw new Exception($"Invaid data: source head count: {sourceHead.Count} not match with source line count: {src.Count}.\nline: {sourceLine}");
            }
            var sourceMap = sourceHead.Zip(src, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);

            Dictionary<string, string> targetMap = new Dictionary<string, string>();

            foreach (var tHead in targetHead) 
            {
                if (sourceMap.ContainsKey(tHead))
                {
                    targetMap.Add(tHead, sourceMap[tHead]);
                }
                else
                {
                    targetMap.Add(tHead, " ");
                }
            }
            return string.Join("\t", targetMap.Values);
        }

        public void AppendNewLineToFile(string filePath, string newLine) 
        {
            Validate(filePath);

            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine(newLine);
            }
        }

        private void Validate(string filePath) 
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new Exception($"filePath should not be empty.");
            }

            if (!File.Exists(filePath))
            {
                throw new Exception($"the file : {filePath} not exist.");
            }
        }

    }
}
