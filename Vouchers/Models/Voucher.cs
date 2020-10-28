using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Dominos.OLO.Vouchers.Models
{
    public class Voucher
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public string ProductCodes { get; set; }

        [JsonIgnore]
        public IList<string> ProductCodeList => ProductCodes.Split(',');
    }
}