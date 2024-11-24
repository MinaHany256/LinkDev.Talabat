﻿using LinkDev.Talabat.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Entites.Orders
{
    public class DeliveryMethod : BaseAuditableEntity<int>
    {
        public required string  ShortName { get; set; }
        public required string Description { get; set; }
        public decimal Cost { get; set; }
        public required string DeliveryTime { get; set; }
    }
}
