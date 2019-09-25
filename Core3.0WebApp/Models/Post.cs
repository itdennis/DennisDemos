using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core3._0WebApp.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string PostContent { get; set; }
        public bool CommentEnabled { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreateOnUtc { get; set; }
        public string ContentAbstract { get; set; }
    }
}
