using Microsoft.EntityFrameworkCore;
using Project.Interfaces;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;

        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        public List<Book> GetAllBooks()
        {
            return _bookRepository.GetAll();
        }

        public Book GetBookById(int id)
        {
            return _bookRepository.GetById(id);
        }

        public void AddBook(Book book)
        {
            _bookRepository.Add(book);
        }

        public void UpdateBook(Book book)
        {
            _bookRepository.Update(book);
        }

        public void DeleteBook(int id)
        {
            _bookRepository.Delete(id);
        }
        public Author GetAuthorByName(string authorName)
        {
            return _authorRepository.GetAll()
                    .FirstOrDefault(a => a.Name!=null && a.Name.ToLower() == authorName.ToLower());
        }
        public List<Book> FilterBooksByTitle(string title)
        {
            return _bookRepository.GetAll()
                    .Where(b => b.Title!=null&& b.Title.ToLower().Contains(title.ToLower()))
                    .ToList();
        }

        public List<Book> FilterBooksByAuthor(string authorName)
        {
            return _bookRepository.GetAll()
                    .Where(b => b.AuthorBooks
                    .Any(ab => ab.Author!=null&& ab.Author.Name!=null && ab.Author.Name.ToLower().Contains(authorName.ToLower())))
                    .ToList();
        }

    }

}
