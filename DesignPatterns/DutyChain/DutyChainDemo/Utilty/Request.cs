using System;
using System.Collections.Generic;
using System.Text;

namespace DutyChainDemo
{
    class Request
    {
        private RequestType requestType;
        public RequestType RequestType { get => this.requestType; set => this.requestType = value; }

        private string requestContent;
        public string RequestContent { get => requestContent; set => requestContent = value; }

        private int number;
        public int Nunber { get => number; set => number = value; }
    }
}
