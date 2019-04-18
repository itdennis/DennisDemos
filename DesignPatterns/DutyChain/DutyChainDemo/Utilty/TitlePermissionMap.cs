using System;
using System.Collections.Generic;
using System.Text;

namespace DutyChainDemo
{
    class TitlePermissionMap
    {
        public static readonly Dictionary<TitleLevel, List<RequestType>> titlePermissionMapping = new Dictionary<TitleLevel, List<RequestType>>()
        {
            { TitleLevel.worker, new List<RequestType>() },
            { TitleLevel.Leader, new List<RequestType>() { RequestType.ReviewCode} },
            { TitleLevel.Manager, new List<RequestType>() { RequestType.ReviewCode, RequestType.OOF} },
            { TitleLevel.Boss, new List<RequestType>() { RequestType.ReviewCode, RequestType.OOF, RequestType.RaiseSalary} },
        };
    }
}
