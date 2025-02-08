using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Interfaces
{
    public interface ILoanItemRepository : IGenericRepository<LoanItem>
    {
        List<LoanItem> GetLoanItemsByLoanId(int loanId);
    }

}
