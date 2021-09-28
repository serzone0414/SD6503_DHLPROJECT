using System;
using System.Collections.Generic;

#nullable disable

namespace SD6503_DHLPROJECT.Models
{
    public partial class TransactionTable
    {
        public int TransactionNumber { get; set; }
        public int ToAccount { get; set; }
        public int FromAccount { get; set; }
        public double LendAmount { get; set; }
        public double PaybackAmount { get; set; }

        public virtual AccountDetail FromAccountNavigation { get; set; }
    }
}
