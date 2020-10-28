using Dominos.OLO.Vouchers.Models;
using System;
using System.Collections.Generic;

namespace Dominos.OLO.Vouchers.Services.Interfaces
{
    public interface IVoucherService
    {
        IEnumerable<Voucher> Get(int count);
        IEnumerable<Voucher> GetWithDiscount(Guid couponId, int count);
        Voucher GetVoucherById(Guid id);
        Voucher GetVoucherByIdWithDiscount(Guid id, Guid couponId);
        Voucher[] GetVouchersByName(string name);
        Voucher[] GetVouchersByNameSearch(string name);
        Voucher GetCheapestVoucherByProductCode(IList<string> productCodes);
    }
}
