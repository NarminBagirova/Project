using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Interfaces
{
    public interface IAuthorBookService
    {
        void AddAuthorBook(AuthorBook authorBook);
        void DeleteAuthorBook(int id);
        void UpdateAuthorBook(AuthorBook authorBook);
        List<AuthorBook> GetAllAuthorBooks();
        AuthorBook GetAuthorBookById(int id);
    }
}
