using System;
using System.Collections.Generic;

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
        public string Name { get; set; }
        public double Balance { get; set; }
        public int? Identifier { get; set; }

        public virtual LoginAccount IdentifierNavigation { get; set; }
        public virtual ICollection<TransactionTable> TransactionTables { get; set; }
    }
}
