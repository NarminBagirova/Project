using Project.Interfaces;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services
{
    public class BorrowerService : IBorrowerService
    {
        private readonly IBorrowerRepository _borrowerRepository;

        public BorrowerService(IBorrowerRepository borrowerRepository)
        {
            _borrowerRepository = borrowerRepository;
        }

        public List<Borrower> GetAllBorrowers()
        {
            return _borrowerRepository.GetAll();
        }

        public Borrower GetBorrowerById(int id)
        {
            return _borrowerRepository.GetById(id);
        }

        public void AddBorrower(Borrower borrower)
        {
            _borrowerRepository.Add(borrower);
        }
        public void UpdateBorrower(Borrower borrower)
        {
            _borrowerRepository.Update(borrower);
        }

        public void RemoveBorrower(int id)
        {
            _borrowerRepository.Delete(id);
        }
        public bool IsEmailUnique(string email)
        {
            return _borrowerRepository.GetAll().All(b => b.Email != email);
        }
    }
}



