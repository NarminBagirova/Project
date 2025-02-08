using Project.Interfaces;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Actions
{
    public class BorrowerAction
    {
        private readonly IBorrowerService _borrowerService;

        public BorrowerAction(IBorrowerService borrowerService)
        {
            _borrowerService = borrowerService;
        }

        public void ShowAllBorrowers()
        {
            var borrowers = _borrowerService.GetAllBorrowers();
            if (borrowers.Count > 0)
            {
                Console.WriteLine("List of Borrowers:");
                foreach (var borrower in borrowers)
                {
                    Console.WriteLine($"ID: {borrower.Id}, Name: {borrower.Name}, Email: {borrower.Email}");
                }
            }
            else
            {
                Console.WriteLine("There are no borrowers.");
            }
        }

        public void AddBorrower()
        {
            Console.Write("Enter Borrower Name: ");
            string? name = Console.ReadLine();
            Console.Write("Enter Borrower Email: ");
            string? email = Console.ReadLine();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email))
            {
                Console.WriteLine("Name and Email cannot be empty!");
                return;
            }

            if (!_borrowerService.IsEmailUnique(email))  
            {
                Console.WriteLine("This email is already in use. Please choose another one.");
                return;
            }

            var borrower = new Borrower { Name = name, Email = email };
            _borrowerService.AddBorrower(borrower);
            Console.WriteLine("New borrower added successfully");
        }

        public void EditBorrower()
        {
            Console.Write("Enter Borrower ID to edit: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID format!");
                return;
            }

            var borrower = _borrowerService.GetBorrowerById(id);
            if (borrower == null)
            {
                Console.WriteLine("Borrower not found!");
                return;
            }

            Console.Write("Enter new name: ");
            string? newName = Console.ReadLine();
            Console.Write("Enter new email: ");
            string? newEmail = Console.ReadLine();

            if (!string.IsNullOrEmpty(newName))
            {
                borrower.Name = newName;
            }
            if (!string.IsNullOrEmpty(newEmail))
            {
                if(_borrowerService.IsEmailUnique(newEmail))
                {
                    borrower.Email=newEmail;
                }
                else
                {
                    Console.WriteLine("This email is already taken. Choose another one.");
                }

            }

            _borrowerService.UpdateBorrower(borrower);
            Console.WriteLine("Borrower updated successfully");
        }

        public void DeleteBorrower()
        {
            Console.Write("Enter Borrower ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID format!");
                return;
            }

            var borrower = _borrowerService.GetBorrowerById(id);
            if (borrower == null)
            {
                Console.WriteLine("Borrower not found!");
                return;
            }
            if (borrower.Books.Any()) 
            {
                Console.WriteLine("This borrower has borrowed books and cannot be deleted.");
                return;
            }

            _borrowerService.RemoveBorrower(id);
            Console.WriteLine("Borrower deleted successfully");
        }

    }
}
