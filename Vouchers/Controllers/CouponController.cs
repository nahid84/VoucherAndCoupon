using Dominos.OLO.Vouchers.Models;
using Dominos.OLO.Vouchers.Services;
using System;
using System.Web.Http;

namespace Dominos.OLO.Vouchers.Controllers
{
    [RoutePrefix("coupon")]
    public class CouponController : ApiController
    {
        private CouponService _couponService;

        [HttpGet]
        [Route("")]
        public Coupon[] Get()
        {
            return Service.GetCoupons();
        }

        [HttpGet]
        [Route("{id}")]
        public Coupon GetById(Guid id)
        {
            return Service.GetCoupon(id);
        }

        internal CouponService Service
        {
            get
            {
                return _couponService ?? (_couponService = new CouponService());
            }
            set
            {
                _couponService = value;
            }
        }
    }
}