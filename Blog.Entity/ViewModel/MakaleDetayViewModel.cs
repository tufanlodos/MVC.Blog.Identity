using Blog.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.ViewModel
{
    public class MakaleDetayViewModel
    {
        public Article Makale { get; set; }
        public Comment Yorum { get; set; }
        public List<Comment> Yorumlar { get; set; }
    }
}
