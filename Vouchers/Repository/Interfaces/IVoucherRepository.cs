using Dominos.OLO.Vouchers.Models;

namespace Dominos.OLO.Vouchers.Repository.Interfaces
{
    public interface IVoucherRepository
    {
        Voucher[] GetVouchers();
    }
}
