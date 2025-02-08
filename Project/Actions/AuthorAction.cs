using Project.Interfaces;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Actions
{
    public class AuthorAction
    {
        private readonly IAuthorService _authorService;

        public AuthorAction(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public void ShowAllAuthors()
        {
            var authors = _authorService.GetAllAuthors();

            if (authors.Count > 0)
            {
                Console.WriteLine("List of Authors:");
                foreach (var author in authors)
                {
                    Console.WriteLine($"ID: {author.Id}, Name: {author.Name}");
                }
            }
            else
                Console.WriteLine("There is no author.");
        }
        public void GetAuthorByName()
        {
            Console.Write("Enter author name: ");
            string? name = Console.ReadLine();
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Author name cannot be empty!");
                return;
            }

            var author = _authorService.GetAuthorByName(name); 
            if (author == null)
            {
                Console.WriteLine("Author not found!");
                return;
            }
            Console.WriteLine($"Author ID: {author.Id}, Name: {author.Name}");
        }

        public void GetAuthorById()
        {
            Console.Write("Enter author ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID format!");
                return;
            }

            var author = _authorService.GetAuthorById(id);
            if (author == null)
            {
                Console.WriteLine("Author not found!");
                return;
            }
            Console.WriteLine($"Author ID: {author.Id}, Name: {author.Name}");
        }

        public void AddAuthor()
        {
            Console.Write("New Author Name: ");
            string? name = Console.ReadLine();
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Author cannot be empty!");
                return;
            }
            var author = new Author { Name = name };
            _authorService.AddAuthor(author);
            Console.WriteLine("New author added successfully");
        }
        public void EditAuthor()
        {
            Console.Write("Enter author ID to edit: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID format!");
                return;
            }

            var author = _authorService.GetAuthorById(id);
            if (author == null)
            {
                Console.WriteLine("Author not found!");
                return;
            }

            Console.Write("Author's new name: ");
            string? newName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(newName))
            {
                Console.WriteLine("New name cannot be empty!");
                return;
            }

            author.Name = newName;
            _authorService.UpdateAuthor(author);
            Console.WriteLine("Author updated successfully.");
        }

        public void DeleteAuthor()
        {
            Console.Write("Enter author ID to delete: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID format!");
                return;
            }

            var author = _authorService.GetAuthorById(id);
            if (author == null)
            {
                Console.WriteLine("Author not found!");
                return;
            }

            _authorService.DeleteAuthor(id);
            Console.WriteLine("Author deleted successfully.");
        }

    }
}




