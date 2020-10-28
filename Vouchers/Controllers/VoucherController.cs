using Dominos.OLO.Vouchers.Models;
using Dominos.OLO.Vouchers.Services.Interfaces;
using System;
using System.Linq;
using System.Web.Http;

namespace Dominos.OLO.Vouchers.Controllers
{
    [RoutePrefix("voucher")]
    public class VoucherController : ApiController
    {
        private const char PRODUCT_LIST_SEPERATOR = ',';

        private readonly IVoucherService _voucherService;

        public VoucherController(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }

        [HttpGet]
        [Route("")]
        public Voucher[] Get(int count = 0)
        {
            return _voucherService.Get(count).ToArray();
        }

        [HttpGet]
        [Route("")]
        public Voucher[] GetWithDiscount(Guid couponId, int count = 0)
        {
            return _voucherService.GetWithDiscount(couponId, count).ToArray();
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetVoucherById(Guid id)
        {
            var result = _voucherService.GetVoucherById(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetVoucherByIdWithDiscount(Guid id, Guid couponId)
        {
            var result = _voucherService.GetVoucherByIdWithDiscount(id, couponId);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [Route("")]
        public Voucher[] GetVouchersByName(string name)
        {
            return _voucherService.GetVouchersByName(name);
        }

        [HttpGet]
        [Route("search/{name}")]
        public Voucher[] GetVouchersByNameSearch(string name)
        {
            return _voucherService.GetVouchersByNameSearch(name);
        }

        [HttpGet]
        [Route("")]
        public Voucher GetCheapestVoucherByProductCode(string productCode)
        {
            return _voucherService.GetCheapestVoucherByProductCode(productCode.Split(PRODUCT_LIST_SEPERATOR));
        }
    }
}