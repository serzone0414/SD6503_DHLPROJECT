using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace SD6503_DHLPROJECT.Models
{
    public partial class AccountDetail
    {
        public AccountDetail()
        {
            TransactionTables = new HashSet<TransactionTable>();
        }

        public int AccountNumber { get; set; }

        [Required(ErrorMessage = "Name cannot be empty")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Balance is required")]
        [Range(0,9999999,ErrorMessage ="Balance should be a resonable number  ;-)")]
        public double Balance { get; set; }
        public int? Identifier { get; set; }

        public virtual LoginAccount IdentifierNavigation { get; set; }
        public virtual ICollection<TransactionTable> TransactionTables { get; set; }
    }
}
