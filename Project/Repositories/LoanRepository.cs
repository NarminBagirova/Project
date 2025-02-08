using Project.Interfaces;
using Project.IRepositories;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repositories
{
    public class LoanRepository : GenericRepository<Loan>, ILoanRepository
    {
        public LoanRepository(AppDbContext context) : base(context) { }

        public List<Loan> GetActiveLoans()
        {
            return _context.Loans.Where(l => l.ReturnDate == null).ToList();
        }

        public void AddLoanItem(LoanItem loanItem)
        {
            _context.LoanItems.Add(loanItem);
            _context.SaveChanges();
        }

    }
}
