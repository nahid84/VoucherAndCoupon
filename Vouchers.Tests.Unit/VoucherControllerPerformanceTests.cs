using Dominos.OLO.Vouchers.Controllers;
using NUnit.Framework;
using System;

namespace Dominos.OLO.Vouchers.Tests.Unit
{
    [TestFixture]
    public class VoucherControllerPerformanceTests
    {
        private readonly VoucherController _controller = new VoucherController();

        [SetUp]
        public void Setup()
        {
            var repository = _controller.VoucherRepo;
            repository.DataFilename = $"{AppDomain.CurrentDomain.BaseDirectory}\\..\\..\\..\\Vouchers\\data.json";
            // This is to pre-load the vouchers.
            repository.GetVouchers();
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