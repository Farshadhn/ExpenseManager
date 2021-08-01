
using Lookif.Layers.Core.MainCore.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.Core.MainCore.Banks
{
    public class Bank : BaseEntity
    {

        /// <summary>
        /// Name of Branch
        /// </summary>
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Code of Branch
        /// </summary>
        [MaxLength(7)]
        public string Code { get; set; }

 
    

    }
}
