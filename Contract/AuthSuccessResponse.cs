using CostApp.Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CostApp.Contract
{
    public class AuthSuccessResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool Admin { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Note { get; set; }
    }
}
