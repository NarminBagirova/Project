using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Interfaces
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        List<Author> GetAuthorsWithBooks();
        List<Author> GetAuthorsByBookId(int bookId);
        Author GetAuthorByName(string name);
    }
}
