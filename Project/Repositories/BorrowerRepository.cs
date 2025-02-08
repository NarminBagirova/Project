using Microsoft.EntityFrameworkCore;
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
    public class BorrowerRepository:GenericRepository<Borrower>, IBorrowerRepository
    {
        public BorrowerRepository(AppDbContext context) : base(context) { }

        public List<Borrower> GetBorrowersWithBooks()
        {
            return _context.Borrowers.Include(b => b.Books).ToList();
        }
    }
}