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
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(AppDbContext context) : base(context) { }

        public List<Author> GetAuthorsWithBooks()
        {
            return _context.Authors.Include(a => a.AuthorBooks)
                   .ThenInclude(ab => ab.Book)
                   .ToList();
        }
        public Author GetAuthorByName(string authorName)
        {
            return _context.Authors
                            .FirstOrDefault(a => a.Name!= null && authorName!=null && a.Name.ToLower() == authorName.ToLower());
        }
        public List<Author> GetAuthorsByBookId(int bookId)
        {
            return _context.AuthorBooks
                .Where(ab => ab.BookId == bookId)
                .Select(ab => ab.Author)
                .ToList();
        }
    }
}



