using Project.Interfaces;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Actions
{
    public class LoanAction
    {
        private readonly ILoanService _loanService;
        private readonly IBookService _bookService;
        private readonly IBorrowerService _borrowerService;

        public LoanAction(ILoanService loanService, IBorrowerService borrowerService, IBookService bookService)
        {
            _loanService = loanService;
            _borrowerService = borrowerService;
            _bookService = bookService;
        }

        public void BorrowBook()
        {
            Console.WriteLine("Available books:");
            var books = _bookService.GetAllBooks();

            if (!books.Any())
            {
                Console.WriteLine("No books available.");
                return;
            }

            foreach (var book in books)
            {
                string availability = book.BorrowerId == 0 ? "Available" : "Not available";
                Console.WriteLine($"Book ID: {book.Id}, Title: {book.Title}, Availability: {availability}");
            }

            Console.Write("Enter the ID of the book you want to borrow: ");
            if (!int.TryParse(Console.ReadLine(), out int bookId))
            {
                Console.WriteLine("Invalid ID format!");
                return;
            }

            var selectedBook = _bookService.GetBookById(bookId);
            if (selectedBook == null || selectedBook.BorrowerId != 0)
            {
                Console.WriteLine("The selected book is not available.");
                return;
            }

            Console.Write("Enter Borrower ID: ");
            if (!int.TryParse(Console.ReadLine(), out int borrowerId))
            {
                Console.WriteLine("Invalid Borrower ID format!");
                return;
            }

            var selectedBorrower = _borrowerService.GetBorrowerById(borrowerId);
            if (selectedBorrower == null)
            {
                Console.WriteLine("Invalid borrower selected.");
                return;
            }

            var loan = new Loan
            {
                BorrowerId = selectedBorrower.Id,
                LoanDate = DateTime.Now,
                MustReturnDate = DateTime.Now.AddDays(15)
            };

            _loanService.AddLoan(loan);
            _loanService.AddLoanItem(new LoanItem { BookId = selectedBook.Id });

            selectedBook.BorrowerId = selectedBorrower.Id;
            _bookService.UpdateBook(selectedBook);

            Console.WriteLine("Loan successfully created!");
        }
        public void ViewBorrowedBooks()
        {
            var loans = _loanService.GetActiveLoans();
            if (loans == null || !loans.Any())
            {
                Console.WriteLine("No active loans found.");
                return;
            }

            Console.WriteLine("List of Borrowed Books:");

            foreach (var loan in loans)
            {
                if (loan.ReturnDate == null)
                {
                    var borrower = _borrowerService.GetBorrowerById(loan.BorrowerId);
                    var books = _loanService.GetBorrowedBooksByBorrower(loan.BorrowerId);

                    if (borrower != null)
                    {
                        Console.WriteLine($"Borrower: {borrower.Name} (ID: {borrower.Id})");
                        foreach (var book in books)
                        {
                            Console.WriteLine($"Book Title: {book.Title}, Borrowed Date: {loan.LoanDate}, Due Date: {loan.MustReturnDate}");
                        }
                    }
                }
            }
        }

        public void ReturnBook(int bookId)
        {
            Console.Write("Enter Borrower ID: ");
            if (!int.TryParse(Console.ReadLine(), out int borrowerId))
            {
                Console.WriteLine("Invalid Borrower ID format!");
                return;
            }

            var loans = _loanService.GetLoanByBorrowerId(borrowerId);
            if (loans == null || !loans.Any())
            {
                Console.WriteLine("No active loans found for this borrower.");
                return;
            }

            foreach (var loan in loans)
            {
                if (loan.ReturnDate != null)
                {
                    Console.WriteLine($"Loan ID {loan.Id} already returned on {loan.ReturnDate}.");
                    continue;
                }

                loan.ReturnDate = DateTime.Now;

                foreach (var loanItem in loan.LoanItems)
                {
                    var book = _bookService.GetBookById(loanItem.BookId);
                    if (book != null)
                    {
                        book.BorrowerId = 0;
                        _bookService.UpdateBook(book);
                        Console.WriteLine($"Book with ID {book.Id} returned successfully.");
                    }
                }

                _loanService.UpdateLoan(loan);
                Console.WriteLine($"Loan ID {loan.Id} successfully returned!");
            }
        }

        public void ViewMostBorrowedBook()
        {
            var book = _loanService.GetMostBorrowedBook();
            Console.WriteLine(book != null ? $"Most Borrowed Book: {book.Title}" : "No data available.");
        }

        public void ViewOverdueBorrowers()
        {
            var borrowers = _loanService.GetOverdueBorrowers();
            if (!borrowers.Any())
            {
                Console.WriteLine("No overdue borrowers.");
                return;
            }
            foreach (var borrower in borrowers)
            {
                Console.WriteLine($"Borrower ID: {borrower.Id}, Name: {borrower.Name}");
            }
        }

        public void ViewBorrowedBooksForBorrower(int borroweId)
        {
            Console.Write("Enter Borrower ID: ");
            if (!int.TryParse(Console.ReadLine(), out int borrowerId))
            {
                Console.WriteLine("Invalid Borrower ID format!");
                return;
            }

            var books = _loanService.GetBorrowedBooksByBorrower(borrowerId);
            if (!books.Any())
            {
                Console.WriteLine("No borrowed books found for this borrower.");
                return;
            }
            foreach (var book in books)
            {
                Console.WriteLine($"Book Title: {book.Title}");
            }
        }
    }
}
