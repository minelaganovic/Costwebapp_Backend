using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CostApp.Contract
{
    public class CreateUserRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Password must have at least 6 and maximum 100 characters.", MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
