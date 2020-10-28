using Dominos.OLO.Vouchers.Controllers;
using Dominos.OLO.Vouchers.Repository;
using Dominos.OLO.Vouchers.Repository.Interfaces;
using Dominos.OLO.Vouchers.Services;
using Dominos.OLO.Vouchers.Services.Interfaces;
using NUnit.Framework;
using System;

namespace Dominos.OLO.Vouchers.Tests.Unit
{
    [TestFixture]
    public class VoucherControllerPerformanceTests
    {
        private VoucherController _controller;

        [SetUp]
        public void Setup()
        {
            IVoucherRepository voucherRepository = new VoucherRepository($"{AppDomain.CurrentDomain.BaseDirectory}\\..\\..\\..\\Vouchers\\data.json");
            voucherRepository.GetVouchers(); // Just to pre-load vouchers

            ICouponRepository couponRepository = new CouponRepository($"{AppDomain.CurrentDomain.BaseDirectory}\\..\\..\\..\\Vouchers\\coupon-data.json");
            couponRepository.GetCoupons(); // Just to pre-load coupons

            ICouponService couponService = new CouponService(couponRepository);

            IVoucherService voucherService = new VoucherService(voucherRepository, couponService);

            _controller = new VoucherController(voucherService);
        }

        [Test]
        public void Get_ShouldBePerformant()
        {
            var startTime = DateTime.Now;

            for (var i = 0; i < 1000; i++)
            {
                _controller.Get();
            }

            var elapsed = DateTime.Now.Subtract(startTime).TotalMilliseconds;
            Assert.LessOrEqual(elapsed, 15000);
        }

        [Test]
        public void Get_ShouldBePerformantWhenReturningASubset()
        {
            var startTime = DateTime.Now;

            for (var i = 0; i < 100000; i++)
            {
                _controller.Get(1000);
            }

            var elapsed = DateTime.Now.Subtract(startTime).TotalMilliseconds;
            Assert.LessOrEqual(elapsed, 5000);
        }

        [Test]
        public void GetCheapestVoucherByProductCode_ShouldBePerformant()
        {
            var startTime = DateTime.Now;

            for (var i = 0; i < 100; i++)
            {
                _controller.GetCheapestVoucherByProductCode("P007D");
            }

            var elapsed = DateTime.Now.Subtract(startTime).TotalMilliseconds;
            Assert.LessOrEqual(elapsed, 15000);
        }
    }
}