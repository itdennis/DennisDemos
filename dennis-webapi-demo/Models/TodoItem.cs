using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dennis_webapi_demo.Models
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
