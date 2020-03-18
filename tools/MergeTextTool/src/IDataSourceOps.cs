using System;
using System.Collections.Generic;
using System.Text;

namespace MergeTextTool.src
{
    public interface IDataSourceOps
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataPath"></param>
        /// <returns></returns>
        string GetHead(string dataPath);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="heads"></param>
        /// <returns></returns>
        HashSet<string> MergeHead(List<string> heads);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataPath"></param>
        /// <returns></returns>
        IAsyncEnumerable<string> GetContentAsync(string dataPath);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="newHeads"></param>
        void CreateNewFile(string filePath, HashSet<string> newHeads);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceHead"></param>
        /// <param name="targetHead"></param>
        /// <param name="sourceLine"></param>
        /// <returns></returns>
        string ConvertToTargetFormat(List<string> sourceHead, HashSet<string> targetHead, string sourceLine);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="newLine"></param>
        void AppendNewLineToFile(string filePath, string newLine);
    }
}
