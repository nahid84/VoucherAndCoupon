using Dominos.OLO.Vouchers.Models;
using Dominos.OLO.Vouchers.Repository.Interfaces;
using Dominos.OLO.Vouchers.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominos.OLO.Vouchers.Services
{
    public class VoucherService : IVoucherService
    {
        private readonly IVoucherRepository _voucherRepo;
        private readonly ICouponService _couponSvc;

        public VoucherService(IVoucherRepository voucherRepo,
                              ICouponService couponSvc)
        {
            _voucherRepo = voucherRepo;
            _couponSvc = couponSvc;
        }

        public IEnumerable<Voucher> Get(int count = 0)
        {
            var vouchers = _voucherRepo.GetVouchers();

            return vouchers.Take(count == 0 ? vouchers.Length : count);
        }

        public IEnumerable<Voucher> GetWithDiscount(Guid couponId, int count)
        {
            var vouchers = Get(count);

            double discount = _couponSvc.GetDiscount(couponId);

            return vouchers.Select(x =>
            {
                if (x.Price > discount)
                    x.Price -= discount;

                return x;
            });
        }

        public Voucher GetVoucherById(Guid id)
        {
            var voucher = _voucherRepo.GetVouchers()
                                      .Where(x => x.Id.Equals(id))
                                      .FirstOrDefault();

            return voucher;
        }

        public Voucher GetVoucherByIdWithDiscount(Guid id, Guid couponId)
        {
            var voucher = GetVoucherById(id);

            double discount = _couponSvc.GetDiscount(couponId);

            if (voucher.Price > discount) voucher.Price -= discount;

            return voucher;
        }

        public Voucher[] GetVouchersByName(string name)
        {
            var vouchers = _voucherRepo.GetVouchers()
                                       .Where(x => x.Name.Equals(name))
                                       .ToArray();

            return vouchers;
        }

        public Voucher[] GetVouchersByNameSearch(string name)
        {
            var vouchers = _voucherRepo.GetVouchers()
                                      .Where(x => x.Name.Contains(name))
                                      .ToArray();

            return vouchers;
        }

        public Voucher GetCheapestVoucherByProductCode(IList<string> productCodes)
        {
            var vouchers = _voucherRepo.GetVouchers()
                                       .Where(x => x.ProductCodeList.Intersect(productCodes).Any())
                                       .OrderBy(x => x.Price);

            return vouchers.FirstOrDefault();
        }
    }
}