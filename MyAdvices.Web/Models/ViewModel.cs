using MyAdvices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyAdvices.Web.Models
{
    public class ViewModel
    {
        public IEnumerable<AdvicesUser> User { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<Liked> Likeds { get; set; }
        public IEnumerable<Note> Notes { get; set; }

    }
}