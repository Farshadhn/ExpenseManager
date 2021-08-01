using ExpenseManager.Core.Enums;
using ExpenseManager.Core.MainCore.Branches;
using Lookif.Layers.Core.MainCore.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseManager.Core.MainCore.Transactions
{
    public class Transaction : BaseEntity
    {



        public DateTime DateTime { get; set; }
        public string SerialNumber { get; set; }
        public string Definition { get; set; }
        public string Beneficiary{ get; set; }
        public double Amount { get; set; }

        public TypeOfTransaction typeOfTransaction{ get; set; }


        public Guid BranchId { get; set; }
        [ForeignKey(nameof(BranchId))]
        public virtual Branch Branch { get; set; }
    }
}
