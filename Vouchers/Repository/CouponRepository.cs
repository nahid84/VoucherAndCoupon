using System;
using System.IO;
using Dominos.OLO.Vouchers.Models;
using Newtonsoft.Json;

namespace Dominos.OLO.Vouchers.Repository
{
    public class CouponRepository
    {
        internal string DataFilename = $"{AppDomain.CurrentDomain.BaseDirectory}coupon-data.json";

        private Coupon[] _coupons;

        public virtual Coupon[] GetCoupons()
        {
            if (_coupons == null)
            {
                var text = File.ReadAllText(DataFilename);
                _coupons = JsonConvert.DeserializeObject<Coupon[]>(text);
            }
            return _coupons;
        }
    }
}