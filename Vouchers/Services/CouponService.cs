using Dominos.OLO.Vouchers.Models;
using Dominos.OLO.Vouchers.Repository.Interfaces;
using Dominos.OLO.Vouchers.Services.Interfaces;
using System;
using System.Linq;

namespace Dominos.OLO.Vouchers.Services
{
    public class CouponService : ICouponService
    {
        private ICouponRepository _couponRepository;

        public CouponService(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        public double GetDiscount(Guid id)
        {
            var coupon = _couponRepository.GetCoupons()
                                          .FirstOrDefault(x=> x.Id.Equals(id));

            if (coupon == null) return 0;

            return coupon.Discount;
        }

        public Coupon[] GetCoupons()
        {
            return _couponRepository.GetCoupons();
        }

        public Coupon GetCoupon(Guid id)
        {
            return _couponRepository.GetCoupons()
                                    .FirstOrDefault(x => x.Id.Equals(id));
        }
    }
}