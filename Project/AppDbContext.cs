using Microsoft.EntityFrameworkCore;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class AppDbContext:DbContext
    {

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<AuthorBook> AuthorBooks { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<LoanItem> LoanItems { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-RK0ANE7\\SQLEXPRESS;Database=PROJECT;Trusted_Connection=True;TrustServerCertificate=True;");
                base.OnConfiguring(optionsBuilder);
            }
        }

    }
}
