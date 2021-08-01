using ExpenseManager.Core.MainCore.Banks;
using Lookif.Layers.Core.MainCore.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseManager.Core.MainCore.Branches
{
    public class Branch : BaseEntity
    {   /// <summary>
        /// Name of Branch
        /// </summary>
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Code of Branch
        /// </summary>
        [MaxLength(7)]
        public string Code { get; set; }



        public Guid BankId { get; set; }
        [ForeignKey(nameof(BankId))]
        public virtual Bank Bank { get; set; }
    }
}
