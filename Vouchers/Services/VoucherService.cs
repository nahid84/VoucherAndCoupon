using Dominos.OLO.Vouchers.Models;
using Dominos.OLO.Vouchers.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominos.OLO.Vouchers.Services
{
    public class VoucherService
    {
        private VoucherRepository _voucherRepository;
        private CouponService _couponService;

        public IEnumerable<Voucher> Get(int count)
        {
            var vouchers = VoucherRepo.GetVouchers();

            return vouchers.Take(count == 0 ? vouchers.Length : 0);
        }

        public IEnumerable<Voucher> GetWithDiscount(Guid couponId, int count)
        {
            var vouchers = Get(count);

            double discount = CouponSvc.GetDiscount(couponId);

            return vouchers.Select(x =>
            {
                if (x.Price > discount)
                    x.Price -= discount;

                return x;
            });
        }

        public Voucher GetVoucherById(Guid id)
        {
            var voucher = VoucherRepo.GetVouchers()
                                     .Where(x => x.Id.Equals(id))
                                     .FirstOrDefault();

            return voucher;
        }

        public Voucher GetVoucherByIdWithDiscount(Guid id, Guid couponId)
        {
            var voucher = GetVoucherById(id);

            double discount = CouponSvc.GetDiscount(couponId);

            if (voucher.Price > discount) voucher.Price -= discount;

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