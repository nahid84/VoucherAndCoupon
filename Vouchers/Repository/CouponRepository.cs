using Dominos.OLO.Vouchers.Models;
using Dominos.OLO.Vouchers.Repository.Interfaces;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Dominos.OLO.Vouchers.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly string _dataFilename;
        private Coupon[] _coupons;

        public CouponRepository(string dataFilename)
        {
            _dataFilename = dataFilename;
        }

        public Coupon[] GetCoupons()
        {
            if (_coupons == null)
            {
                try
                {
                    var text = File.ReadAllText(_dataFilename);
                    _coupons = JsonConvert.DeserializeObject<Coupon[]>(text);
                }
                catch (Exception) { }
            }
            return _coupons;
        }
    }
}