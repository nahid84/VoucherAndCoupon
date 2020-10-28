using Dominos.OLO.Vouchers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominos.OLO.Vouchers.Repository.Interfaces
{
    public interface ICouponRepository
    {
        Coupon[] GetCoupons();
    }
}
