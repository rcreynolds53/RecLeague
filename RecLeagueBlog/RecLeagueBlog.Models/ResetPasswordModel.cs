using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecLeagueBlog.Models
{
    public class ResetPasswordModel
    {
        [Required(AllowEmptyStrings =false, ErrorMessage ="Please enter an email")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings =false, ErrorMessage ="Please enter a new password")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The password must be at least {2} characters long.", MinimumLength = 6)]
        public string NewPassword { get; set; }

    }
}
