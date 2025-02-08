using Project.Interfaces;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanItemRepository _loanItemRepository;
        private readonly ILoanRepository _loanRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IBorrowerRepository _borrowerRepository;

        public LoanService(ILoanItemRepository loanItemRepository, ILoanRepository loanRepository, IBookRepository bookRepository, IBorrowerRepository borrowerRepository)
        {
            _loanItemRepository = loanItemRepository;
            _loanRepository = loanRepository;
            _bookRepository = bookRepository;
            _borrowerRepository = borrowerRepository;
        }
        public void AddLoan(Loan loan)
        {
            _loanRepository.Add(loan);  
        }

        public void AddLoanItem(LoanItem loanItem)
        {
            _loanItemRepository.Add(loanItem);
        }

        public void BorrowBook(int borrowerId, int bookId)
        {
            var borrower = _borrowerRepository.GetById(borrowerId);
            var book = _bookRepository.GetById(bookId);

            if (borrower == null || book == null)
            {
                Console.WriteLine("Invalid borrower or book.");
                return;
            }

            if (book.BorrowerId != 0)
            {
                Console.WriteLine("Book is already borrowed.");
                return;
            }

            var loan = new Loan
            {
                BorrowerId = borrowerId,
                LoanDate = DateTime.Now,
                MustReturnDate = DateTime.Now.AddDays(15),
                LoanItems = new List<LoanItem> { new LoanItem { BookId = bookId } }
            };

            _loanRepository.Add(loan);
            book.BorrowerId = borrowerId;
            _bookRepository.Update(book);

            Console.WriteLine("Book borrowed successfully!");
        }

        public void ReturnBook(int loanId)
        {
            var loan = _loanRepository.GetById(loanId);
            if (loan == null || loan.ReturnDate != null)
            {
                Console.WriteLine("Loan not found or already returned.");
                return;
            }

            loan.ReturnDate = DateTime.Now;
            _loanRepository.Update(loan);

            foreach (var loanItem in loan.LoanItems)
            {
                var book = _bookRepository.GetById(loanItem.BookId);
                if (book != null)
                {
                    book.BorrowerId = 0;
                    _bookRepository.Update(book);
                }
            }

            Console.WriteLine("Book returned successfully!");
        }
        public void UpdateLoan(Loan loan)
        {
            _loanRepository.Update(loan);
        }

        public List<Loan> GetActiveLoans()
        {

            return _loanRepository.GetActiveLoans();
        }

        public List<Loan> GetOverdueLoans()
        {
            var overdueLoans = _loanRepository.GetAll()
                .Where(l => l.ReturnDate == null && l.MustReturnDate < DateTime.Now)
                .ToList();
            return overdueLoans;
        }
        public Book GetMostBorrowedBook()
        {
            var mostBorrowedBookId = _loanItemRepository.GetAll()
                .GroupBy(l => l.BookId)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();

            return _bookRepository.GetById(mostBorrowedBookId);
        }

        public List<Borrower> GetOverdueBorrowers()
        {
            var overdueBorrowers = _loanRepository.GetAll()
                .Where(l => l.MustReturnDate < DateTime.Now && l.ReturnDate == null)
                .Select(l => l.BorrowerId)
                .Distinct()
                .ToList();

            return _borrowerRepository.GetAll()
                .Where(b => overdueBorrowers.Contains(b.Id))
                .ToList();
        }

        public List<Book> GetBorrowedBooksByBorrower(int borrowerId)
        {
            var loanedBookIds = _loanItemRepository.GetAll()
                                .Where(li => li.Loan.BorrowerId == borrowerId && li.Loan.ReturnDate == null)
                                .Select(li => li.BookId)
                                .Distinct() 
                                .ToList();

            return _bookRepository.GetAll().Where(b => loanedBookIds.Contains(b.Id)).ToList();
        }
        public List<Loan> GetLoanByBorrowerId(int borrowerId)
        {
            return _loanRepository.GetAll()
                .Where(l => l.BorrowerId == borrowerId)
                .ToList();
        }
    }
}





