using Project.Interfaces;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Actions
{
    public class BookAction
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly IAuthorBookService _authorBookService;


        public BookAction(IBookService bookService, IAuthorService authorService, IAuthorBookService authorBookService)
        {
            _bookService = bookService;
            _authorService= authorService;
            _authorBookService = authorBookService;
        }
        public void AddBook()
        {
            Console.Write("Enter book title: ");
            string? title = Console.ReadLine();

            Console.Write("Enter published year: ");
            if (!int.TryParse(Console.ReadLine(), out int publishedYear))
            {
                Console.WriteLine("Invalid year format.");
                return;
            }

            Console.Write("Add description:");
            string? description=Console.ReadLine().Trim();

            if(description==null)
            {
                Console.WriteLine("Description cannot be null");
                return;
            }

            Console.Write("Enter authors and split them with comma:");
            var authorNames = Console.ReadLine()?.Split(',')
                            .Select(name => name.Trim())
                            .Where(name => !string.IsNullOrWhiteSpace(name))
                            .ToList();

            if (authorNames == null || authorNames.Count == 0)
            {
                Console.WriteLine("At least one author is required.");
                return;
            }

            var newBook = new Book
            {
                Title = title,
                PublishedYear = publishedYear,
                Description = description
            };

            _bookService.AddBook(newBook);

            foreach (var authorName in authorNames)
            {
                var author = _authorService.GetAuthorByName(authorName);
                if (author == null)
                {
                    Console.WriteLine($"Author '{authorName}' not found. Creating new author...");
                    author = new Author { Name = authorName };
                    _authorService.AddAuthor(author);

                    author = _authorService.GetAuthorByName(authorName);
                }
                else
                {
                    Console.WriteLine($"Author {authorName} not found. Please create the author first.");
                }
            }

            _bookService.AddBook(newBook);
            Console.WriteLine("Book added successfully.");
        }

        public void EditBook()
        {
            Console.Write("Enter book ID to edit: ");
            if (!int.TryParse(Console.ReadLine(), out int bookId))
            {
                Console.WriteLine("Invalid book ID format.");
                return;
            }

            var book = _bookService.GetBookById(bookId);

            if (book == null)
            {
                Console.WriteLine("Book not found.");
                return;
            }

            Console.WriteLine($"Editing book: {book.Title}");

            Console.Write("Enter new title: ");
            string? newTitle = Console.ReadLine();
            if (!string.IsNullOrEmpty(newTitle))
            {
                book.Title = newTitle;
            }

            Console.Write("Enter new published year: ");
            if (int.TryParse(Console.ReadLine(), out int newYear))
            {
                book.PublishedYear = newYear;
            }

            Console.Write("Enter the new author name: ");
            string? newAuthorName = Console.ReadLine();
            if (!string.IsNullOrEmpty(newAuthorName))
            {
                var author = _authorService.GetAuthorByName(newAuthorName);

                if (author != null)
                {
                    var authorBook = new AuthorBook
                    {
                        AuthorId = author.Id,
                        BookId = book.Id
                    };

                    book.AuthorBooks.Add(authorBook); 
                }
                else
                {
                    Console.WriteLine($"Author '{newAuthorName}' not found. Please create the author.");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Please provide a valid author name.");
            }
        }
        public void DeleteBook()
        {
            Console.Write("Enter book ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int bookId))
            {
                Console.WriteLine("Invalid book ID format.");
                return;
            }

            var book = _bookService.GetBookById(bookId);

            if (book == null)
            {
                Console.WriteLine("Book not found.");
                return;
            }

            Console.WriteLine($"Are you sure you want to delete the book '{book.Title}'? (y/n)");
            var confirm = Console.ReadLine();
            if (confirm?.ToLower() == "y")
            {
                _bookService.DeleteBook(bookId);
                Console.WriteLine("Book deleted successfully.");
            }
            else
            {
                Console.WriteLine("Book deletion cancelled.");
            }
        }
        public void ShowAllBooks()
        {
            var books = _bookService.GetAllBooks();

            if (!books.Any())
            {
                Console.WriteLine("There are no books.");
                return;
            }

            foreach (var book in books)
            {
                var authors=_authorService.GetAuthorsByBookId(book.Id);

                if (authors.Any())
                {
                    string authorNames = string.Join(",", authors.Select(async => async.Name));
                    Console.WriteLine($"Book ID: {book.Id}, Title: {book.Title}, PublishedYear:{book.PublishedYear}, authors: {authorNames}");
                }
            }
        }
        public void GetBookById()
        {
            Console.Write("Enter author ID:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID format!");
                return;
            }
            var book = _bookService.GetBookById(id);

            if (book == null)
            {
                Console.WriteLine("Book not found");
                return;
            }
            Console.WriteLine($"Book ID: {book.Id}");
            Console.WriteLine($"Title: {book.Title}");
            Console.WriteLine($"Published Year: {book.PublishedYear}");
            Console.WriteLine("Authors: ");

            var authors = _authorService.GetAuthorsByBookId(id);

            if (authors.Count > 0)
            {
                foreach (var author in authors)
                {
                    Console.WriteLine($"- {author.Name}");
                }
            }
            else
            {
                Console.WriteLine("No authors assigned.");
            }
        }

        public void FilterBooksByTitle()
        {
            Console.Write("Enter book title to search: ");
            string? title = Console.ReadLine();
            var books = _bookService.FilterBooksByTitle(title??"");

            if (books.Count() > 0)
            {
                Console.WriteLine("Books found:");
                foreach (var book in books)
                {
                    Console.WriteLine($"Title: {book.Title}, Published Year: {book.PublishedYear}");
                }
            }
            else
            {
                Console.WriteLine("No books found with the given title.");
            }
        }

        public void FilterBooksByAuthor()
        {
            Console.Write("Enter author name to search: ");
            string? authorName = Console.ReadLine();
            var books = _bookService.FilterBooksByAuthor(authorName?? "");

            if (books.Any())
            {
                Console.WriteLine("Books found:");
                foreach (var book in books)
                {
                    var authors=book.AuthorBooks.Select(x => x.Author).ToList();
                    Console.WriteLine($"Title: {book.Title}, Author(s): {string.Join(", ", authors.Select(a => a.Name))}");
                }
            }
            else
            {
                Console.WriteLine("No books found with the given author.");
            }
        }
    }
}
