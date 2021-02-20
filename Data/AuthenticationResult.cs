using CostApp.Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CostApp.Data
{
    public class AuthenticationResult
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public bool Admin { get; set; }
    }
}
