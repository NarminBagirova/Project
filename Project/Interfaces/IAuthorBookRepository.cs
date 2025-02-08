using Microsoft.EntityFrameworkCore;
using Project.IRepositories;
using Project.Models;
using Project.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Interfaces
{
    public interface IAuthorBookRepository : IGenericRepository<AuthorBook>
    {
        List<AuthorBook> GetBooksByAuthorId(int authorId);

        List<AuthorBook> GetAuthorsByBookId(int bookId);
    }
}
