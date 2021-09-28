using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace SD6503_DHLPROJECT.Models
{
    public partial class LoginAccount
    {
        public LoginAccount()
        {
            AccountDetails = new HashSet<AccountDetail>();
        }
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }
        public int Identifier { get; set; }

        public virtual ICollection<AccountDetail> AccountDetails { get; set; }
    }
}
