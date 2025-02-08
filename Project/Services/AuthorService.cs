using Project.Interfaces;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services
{
    public class AuthorService:IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public List<Author> GetAllAuthors()
        {
            return _authorRepository.GetAll();
        }

        public Author GetAuthorById(int id)
        {
            return _authorRepository.GetById(id);
        }
        public List<Author> GetAuthorsByBookId(int bookId)
        {
            return _authorRepository.GetAuthorsByBookId(bookId);
        }

        public void AddAuthor(Author? author)
        {
            var existingAuthor = _authorRepository.GetAuthorByName(author.Name);

            if (existingAuthor != null)
            {
                throw new InvalidOperationException("Author with this name already exists.");
            }
            Console.Write($"Author '{author.Name}' not found. Do you want to add? (yes/no): ");
            string? response = Console.ReadLine()?.Trim().ToLower();

            if (response == "yes")
            {
                _authorRepository.Add(author);
                Console.WriteLine($"Author '{author.Name}' added successfully.");
            }
            else
            {
                Console.WriteLine("Operation cancelled.");
            }
        }

        public void UpdateAuthor(Author author)
        {
            var existingAuthor = _authorRepository.GetById(author.Id);
            if (existingAuthor == null)
            {
                throw new InvalidOperationException("Author not found.");
            }

            _authorRepository.Update(author);
        }

        public void DeleteAuthor(int id)
        {
            var author = _authorRepository.GetById(id);
            if (author == null)
            {
                throw new InvalidOperationException("Author not found.");
            }

            _authorRepository.Delete(id);

        }
        public Author GetAuthorByName(string authorName)
        {
            return _authorRepository.GetAuthorByName(authorName);
        }

    }
}
