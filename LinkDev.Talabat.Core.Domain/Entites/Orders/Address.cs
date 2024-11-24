using LinkDev.Talabat.Core.Domain.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Entites.Orders
{
    public class Address
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Street { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser User { get; set; } = null!;
    }
}
