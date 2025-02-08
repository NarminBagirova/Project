using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Interfaces
{
    public interface ILoanService
    {
        void BorrowBook(int borrowerId, int bookId);
        void ReturnBook(int loanId);
        void AddLoan(Loan loan);
        void AddLoanItem(LoanItem loanItem);
        void UpdateLoan(Loan loan);
        List<Loan> GetActiveLoans();
        List<Loan> GetOverdueLoans();
        Book GetMostBorrowedBook();
        List<Borrower> GetOverdueBorrowers();
        List<Book> GetBorrowedBooksByBorrower(int borrowerId);
        List<Loan> GetLoanByBorrowerId(int borrowerId);

    }
}
