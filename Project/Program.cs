using Microsoft.EntityFrameworkCore;
using Project.Actions;
using Project.Interfaces;
using Project.Repositories;
using Project.Services;

namespace Project
{
    internal class Program
    {
        private static IBookService? _bookService;
        private static IBorrowerService? _borrowerService;
        private static ILoanService? _loanService;
        private static IAuthorService? _authorService;
        private static BorrowerAction? _borrowerAction;
        private static LoanAction? _loanAction;
        private static BookAction? _bookAction;
        private static AuthorAction? _authorAction;
        private static AuthorBookService _authorBookService;

        static void Main(string[] args)
        {
            InitializeServices();

            while (true)
            {
                Console.WriteLine("Library Management System");
                Console.WriteLine("1 - Author actions");
                Console.WriteLine("2 - Book actions");
                Console.WriteLine("3 - Borrower actions");
                Console.WriteLine("4 - Loan actions");
                Console.WriteLine("0 - Exit");
                Console.Write("Please choose an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AuthorActions();
                        break;
                    case "2":
                        BookActions();
                        break;
                    case "3":
                        BorrowerActions();
                        break;
                    case "4":
                        LoanActions();
                        break;
                    case "0":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }

        private static void InitializeServices()
        {
            var context = new AppDbContext(); 
            var bookRepository = new BookRepository(context);
            var borrowerRepository = new BorrowerRepository(context);
            var loanRepository = new LoanRepository(context);
            var loanItemRepository= new LoanItemRepository(context);
            var authorRepository = new AuthorRepository(context);
            var authorBookRepository = new AuthorBookRepository(context);

            _bookService = new BookService(bookRepository, authorRepository);
            _borrowerService = new BorrowerService(borrowerRepository);
            _loanService = new LoanService(loanItemRepository,loanRepository, bookRepository, borrowerRepository);
            _authorService = new AuthorService(authorRepository);
            _authorBookService= new AuthorBookService(authorBookRepository);

            _bookAction = new BookAction(_bookService, _authorService, _authorBookService);
            _borrowerAction = new BorrowerAction(_borrowerService);
            _loanAction = new LoanAction(_loanService, _borrowerService, _bookService);
            _authorAction = new AuthorAction(_authorService);
        }

        private static void AuthorActions()
        {
            Console.WriteLine("Author Actions:");
            Console.WriteLine("1 - View All Authors");
            Console.WriteLine("2 - Add Author");
            Console.WriteLine("3 - Edit Author");
            Console.WriteLine("4 - Delete Author");
            Console.WriteLine("0 - Back");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    _authorAction.ShowAllAuthors();
                    break;
                case "2":
                    _authorAction.AddAuthor();
                    break;
                case "3":
                    _authorAction.EditAuthor();
                    break;
                case "4":
                    _authorAction.DeleteAuthor();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid choice, try again.");
                    break;
            } 
        }

        private static void BookActions()
        {
            Console.WriteLine("Book Actions:");
            Console.WriteLine("1 - View All Books");
            Console.WriteLine("2 - Add Book");
            Console.WriteLine("3 - Edit Book");
            Console.WriteLine("4 - Delete Book");
            Console.WriteLine("0 - Exit");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    _bookAction.ShowAllBooks();
                    break;
                case "2":
                    _bookAction.AddBook();
                    break;
                case "3":
                    _bookAction.EditBook();
                    break;
                case "4":
                    _bookAction.DeleteBook();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid choice, try again.");
                    break;
            }
        }

        private static void BorrowerActions()
        {
            Console.WriteLine("Borrower Actions:");
            Console.WriteLine("1 - View All Borrowers");
            Console.WriteLine("2 - Add Borrower");
            Console.WriteLine("3 - Edit Borrower");
            Console.WriteLine("4 - Delete Borrower");
            Console.WriteLine("0 - Exit");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    _borrowerAction.ShowAllBorrowers();
                    break;
                case "2":
                    _borrowerAction.AddBorrower();
                    break;
                case "3":
                    _borrowerAction.EditBorrower();
                    break;
                case "4":
                    _borrowerAction.DeleteBorrower();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid choice, try again.");
                    break;
            }
        }

        private static void LoanActions()
        {
            Console.WriteLine("Loan Actions:");
            Console.WriteLine("1 - Borrow Book");
            Console.WriteLine("2 - Return Book");
            Console.WriteLine("3 - View Most Borrowed Book");
            Console.WriteLine("4 - View Overdue Borrowers");
            Console.WriteLine("5 - View Borrowed Books for a Borrower");
            Console.WriteLine("0 - Exit");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    _loanAction.BorrowBook();
                    break;
                case "2":
                    Console.Write("Enter Loan ID to return: ");
                    if (int.TryParse(Console.ReadLine(), out int loanId))
                    {
                        _loanAction.ReturnBook(loanId);
                    }
                    else
                    {
                        Console.WriteLine("Invalid Loan ID.");
                    }
                    break;
                case "3":
                    _loanAction.ViewMostBorrowedBook();
                    break;
                case "4":
                    _loanAction.ViewOverdueBorrowers();
                    break;
                case "5":
                    Console.Write("Enter Borrower ID to view borrowed books: ");
                    if (int.TryParse(Console.ReadLine(), out int borrowerId))
                    {
                        _loanAction.ViewBorrowedBooksForBorrower(borrowerId);
                    }
                    else
                    {
                        Console.WriteLine("Invalid Borrower ID.");
                    }
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid choice, try again.");
                    break;
            }
        }
    }
}


