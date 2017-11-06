using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecLeagueBlog.Models
{
    public class UpdatePasswordModel
    {
        //[Required]
        //public string UserId { get; set; }

        [Required(AllowEmptyStrings =false, ErrorMessage ="Password is Required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(AllowEmptyStrings =false, ErrorMessage ="Please confirm your password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage ="The passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
