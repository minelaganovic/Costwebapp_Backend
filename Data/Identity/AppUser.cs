using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CostApp.Data.Identity
{
    public class AppUser : IdentityUser<string>
    {
        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(3000)]
        public string Note { get; set; }
    }
}
