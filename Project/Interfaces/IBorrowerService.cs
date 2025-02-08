using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Interfaces
{
    public interface IBorrowerService
    {
        List<Borrower>GetAllBorrowers();
        Borrower GetBorrowerById(int id);
        void AddBorrower(Borrower borrower);
        void UpdateBorrower(Borrower borrower);
        void RemoveBorrower(int id);
        bool IsEmailUnique(string email);
    }
}
