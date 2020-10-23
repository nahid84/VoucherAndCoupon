using Dominos.OLO.Vouchers.Models;
using Dominos.OLO.Vouchers.Repository;
using System;
using System.Linq;

namespace Dominos.OLO.Vouchers.Services
{
    public class CouponService
    {
        private CouponRepository _couponRepository;

        public double GetDiscount(Guid id)
        {
            var coupon = Repository.GetCoupons()
                                   .FirstOrDefault(x=> x.Id.Equals(id));

            if (coupon == null) return 0;

            return coupon.Discount;
        }

        public Coupon[] GetCoupons()
        {
            return Repository.GetCoupons();
        }

        public Coupon GetCoupon(Guid id)
        {
            return Repository.GetCoupons()
                             .FirstOrDefault(x => x.Id.Equals(id));
        }

        internal CouponRepository Repository
        {
            get
            {
                return _couponRepository ?? (_couponRepository = new CouponRepository());
            }
            set
            {
                _couponRepository = value;
            }
        }
    }
}