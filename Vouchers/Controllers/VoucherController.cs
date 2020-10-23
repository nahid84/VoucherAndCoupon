using Dominos.OLO.Vouchers.Models;
using Dominos.OLO.Vouchers.Repository;
using Dominos.OLO.Vouchers.Services;
using System;
using System.Linq;
using System.Web.Http;

namespace Dominos.OLO.Vouchers.Controllers
{
    [RoutePrefix("voucher")]
    public class VoucherController : ApiController
    {
        private VoucherRepository _voucherRepository;
        private CouponService _couponService;

        [HttpGet]
        [Route("{count}")]
        public Voucher[] Get(int count = 0)
        {
            var vouchers = VoucherRepo.GetVouchers();

            return vouchers.Take(count == 0 ? vouchers.Length : count)
                           .ToArray();
        }

        [HttpGet]
        [Route("{count}/coupon/{couponId}")]
        public Voucher[] GetWithDiscount(Guid couponId, int count = 0)
        {
            var vouchers = Get(count);

            double discount = CouponSvc.GetDiscount(couponId);

            return vouchers.Select(x =>
            {
                if (x.Price > discount)
                    x.Price -= discount;

                return x;

            }).ToArray();
        }

        [HttpGet]
        [Route("{id}")]
        public Voucher GetVoucherById(Guid id)
        {
            var voucher = VoucherRepo.GetVouchers()
                                     .Where(x => x.Id.Equals(id))
                                     .FirstOrDefault();

            return voucher;
        }

        [HttpGet]
        [Route("{id}/coupon/{couponId}")]
        public Voucher GetVoucherByIdWithDiscount(Guid id, Guid couponId)
        {
            var voucher = GetVoucherById(id);

            double discount = CouponSvc.GetDiscount(couponId);

            if (voucher.Price > discount) voucher.Price -= discount;

            return voucher;
        }

        [HttpGet]
        [Route("title/{name}")]
        public Voucher[] GetVouchersByName(string name)
        {
            var vouchers = VoucherRepo.GetVouchers()
                                      .Where(x => x.Name.Equals(name))
                                      .ToArray();

            return vouchers;
        }

        [HttpGet]
        [Route("search/{name}")]
        public Voucher[] GetVouchersByNameSearch(string name)
        {
            var vouchers = VoucherRepo.GetVouchers()
                                      .Where(x => x.Name.Contains(name))
                                      .ToArray();

            return vouchers;
        }

        [HttpGet]
        [Route("cheapest/{productCode}")]
        public Voucher GetCheapestVoucherByProductCode(string productCode)
        {
            var voucher = VoucherRepo.GetVouchers()
                                      .Where(x => x.ProductCodes.Contains(productCode))
                                      .OrderBy(x => x.Price)
                                      .FirstOrDefault();

            return voucher;
        }

        internal VoucherRepository VoucherRepo
        {
            get
            {
                return _voucherRepository ?? (_voucherRepository = new VoucherRepository());
            }
            set
            {
                _voucherRepository = value;
            }
        }

        internal CouponService CouponSvc
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