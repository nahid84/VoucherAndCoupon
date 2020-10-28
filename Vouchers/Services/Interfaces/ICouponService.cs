using Dominos.OLO.Vouchers.Models;
using System;

namespace Dominos.OLO.Vouchers.Services.Interfaces
{
    public interface ICouponService
    {
        double GetDiscount(Guid id);
        Coupon[] GetCoupons();
        Coupon GetCoupon(Guid id);
    }
}
