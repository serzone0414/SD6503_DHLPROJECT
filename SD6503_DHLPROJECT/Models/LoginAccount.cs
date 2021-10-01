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
        [StringLength (10,MinimumLength =4,ErrorMessage = ("Username should be between 4 to 10 characters"))]
        public string Username { get; set; }

        [StringLength(20, MinimumLength = 4, ErrorMessage = ("Password should be between 4 to 20 characters"))]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }
        public int Identifier { get; set; }

        public virtual ICollection<AccountDetail> AccountDetails { get; set; }
    }
}
