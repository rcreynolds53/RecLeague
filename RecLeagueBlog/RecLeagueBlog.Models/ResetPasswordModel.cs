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
        [Required(AllowEmptyStrings =false)]
        public string Email { get; set; }

        [Required(AllowEmptyStrings =false)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

    }
}
