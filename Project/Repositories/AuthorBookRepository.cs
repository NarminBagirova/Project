using Microsoft.EntityFrameworkCore;
using Project.Interfaces;
using Project.IRepositories;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repositories
{
    public class AuthorBookRepository:GenericRepository<AuthorBook>,IAuthorBookRepository
    {
        public AuthorBookRepository(AppDbContext context) : base(context) { }

        public List<AuthorBook> GetAuthorsByBookId(int bookId)
        {
            return _dbSet.Where(ab => ab.BookId == bookId).ToList();
        }

        public List<AuthorBook> GetBooksByAuthorId(int authorId)
        {
            return _dbSet.Where(ab => ab.AuthorId == authorId).ToList();
        }
    }
}
