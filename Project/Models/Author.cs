﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<AuthorBook> AuthorBooks { get; set; } = new List<AuthorBook>();
    }
}
