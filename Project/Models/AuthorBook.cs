using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    public class AuthorBook
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public Author? Author { get; set; }

        public int BookId { get; set; }
        public Book? Book { get; set; }
        public List<AuthorBook> AuthorBooks { get; set; } = new List<AuthorBook>();
    }

}
