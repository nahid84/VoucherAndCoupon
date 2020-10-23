using System;

namespace Dominos.OLO.Vouchers.Models
{
    public class Coupon
    {
        public Guid Id { get; set; }

        public string Type { get; set; }

        public double Discount { get; set; }
    }
}