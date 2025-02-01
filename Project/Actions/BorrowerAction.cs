using Project.Interfaces;
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
    }
}
