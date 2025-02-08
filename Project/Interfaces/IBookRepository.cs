using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        List<Book> GetBooksWithAuthors();
        List<Book> FilterBooksByTitle(string title);
        List<Book> FilterBooksByAuthor(string authorName);
    }
   
}
