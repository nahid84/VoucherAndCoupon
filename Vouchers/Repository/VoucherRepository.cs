using Dominos.OLO.Vouchers.Models;
using Dominos.OLO.Vouchers.Repository.Interfaces;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Dominos.OLO.Vouchers.Repository
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly string _dataFilename;
        private Voucher[] _vouchers;

        public VoucherRepository(string dataFilename)
        {
            _dataFilename = dataFilename;
        }

        public Voucher[] GetVouchers()
        {
            if (_vouchers == null)
            {
                try
                {
                    var text = File.ReadAllText(_dataFilename);
                    _vouchers = JsonConvert.DeserializeObject<Voucher[]>(text);
                }
                catch (Exception) { }
            }
            return _vouchers;
        }
    }
}