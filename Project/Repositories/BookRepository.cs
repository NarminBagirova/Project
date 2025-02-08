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
    public class BookRepository:GenericRepository<Book>, IBookRepository
    {
        public BookRepository(AppDbContext context) : base(context) { }

        public List<Book> GetBooksWithAuthors()
        {
            return _context.Books
                    .Include(b => b.AuthorBooks) 
                    .ThenInclude(ab => ab.Author) 
                    .ToList();
        }

        public List<Book> FilterBooksByTitle(string title)
        {
            return _context.Books
                    .Where(b => b.Title.ToLower().Contains(title.ToLower())) 
                    .Include(b => b.AuthorBooks) 
                    .ThenInclude(ab => ab.Author) 
                    .ToList();
        }

        public List<Book> FilterBooksByAuthor(string authorName)
        {
            return _context.Books
                    .Where(b => b.AuthorBooks.Any(ab => ab.Author.Name.ToLower().Contains(authorName.ToLower())))
                    .Include(b => b.AuthorBooks)  
                    .ThenInclude(ab => ab.Author)  
                    .ToList();
        }
    }
}
