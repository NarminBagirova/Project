using Microsoft.EntityFrameworkCore;
using Project.Interfaces;
using Project.IRepositories;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Project.Repositories.LoanItemRepository;

namespace Project.Repositories
{
    public class LoanItemRepository : GenericRepository<LoanItem>, ILoanItemRepository
    {
        public LoanItemRepository(AppDbContext context) : base(context) { }

        public List<LoanItem> GetLoanItemsByLoanId(int loanId)
        {
            return _context.LoanItems.Where(li => li.LoanId == loanId).ToList();
        }
    }

}
