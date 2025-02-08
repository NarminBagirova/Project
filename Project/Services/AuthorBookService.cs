using Project.Interfaces;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services
{

    public class AuthorBookService : IAuthorBookService
    {
        private readonly IGenericRepository<AuthorBook> _authorBookRepository;

        public AuthorBookService(IGenericRepository<AuthorBook> authorBookRepository)
        {
            _authorBookRepository = authorBookRepository;
        }

        public void AddAuthorBook(AuthorBook authorBook)
        {
            _authorBookRepository.Add(authorBook);
        }

        public void DeleteAuthorBook(int id)
        {
            _authorBookRepository.Delete(id);
        }

        public void UpdateAuthorBook(AuthorBook authorBook)
        {
            _authorBookRepository.Update(authorBook);
        }

        public List<AuthorBook> GetAllAuthorBooks()
        {
            return _authorBookRepository.GetAll();
        }

        public AuthorBook GetAuthorBookById(int id)
        {
            return _authorBookRepository.GetById(id);
        }
    }
}
