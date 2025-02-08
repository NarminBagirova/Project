﻿using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Interfaces
{
    public interface ILoanRepository:IGenericRepository <Loan>
    {
        List<Loan> GetActiveLoans();
    }
}
