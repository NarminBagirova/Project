using Project.Interfaces;
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

        public BookAction(IBookService bookService)
        {
            _bookService = bookService;
        }
        public void ShowAllBooks()
        {
            var books = _bookService.GetAllBooks();

            if (books.Count > 0)
            {
                Console.WriteLine("List of books: ");
                foreach (var book in books)
                {
                    Console.WriteLine($"Book ID: {book.Id}, Title: {book.Title}, Description: {book.PublishedYear}, Author: {book.Authors}");
                }
            }
            else
                Console.WriteLine("There is no book.");
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

            if (book.Authors.Count > 0)
            {
                foreach (var author in book.Authors)
                {
                    Console.WriteLine($"- {author.Name}");
                }
            }
            else
            {
                Console.WriteLine("No authors assigned.");
            }
        }

    }
}
