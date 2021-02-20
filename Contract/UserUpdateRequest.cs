using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CostApp.Contract
{
    public class UserUpdateRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [MaxLength(3000)]
        public string Note { get; set; }
    }
}
