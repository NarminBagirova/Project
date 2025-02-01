using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    public class LoanItem
    {
        public int Id { get; set; }
        public int BookId {  get; set; }
        public Book Book { get; set; }
    }
}
