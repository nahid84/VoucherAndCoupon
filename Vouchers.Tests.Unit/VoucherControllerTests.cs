using System;
using System.Collections.Generic;
using Dominos.OLO.Vouchers.Controllers;
using Dominos.OLO.Vouchers.Models;
using Dominos.OLO.Vouchers.Repository;
using NSubstitute;
using NUnit.Framework;

namespace Dominos.OLO.Vouchers.Tests.Unit
{
    [TestFixture]
    public class VoucherControllerTests
    {
        private readonly VoucherController _controller = new VoucherController();

        private readonly VoucherRepository _repository = Substitute.For<VoucherRepository>();

        private readonly List<Voucher> _vouchers = new List<Voucher>();

        [SetUp]
        public void Setup()
        {
            _repository.GetVouchers().Returns(info => _vouchers.ToArray());
            _controller.VoucherRepo = _repository;
        }

        [Test, Order(3)]
        public void Get_ShouldReturnRequestedNumberOfVouchers()
        {
            for (var i = 0; i < 1000; i++)
            {
                _vouchers.Add(new Voucher
                {
                    Id = new Guid()
                });
            }

            var result = _controller.Get(100);

            Assert.AreEqual(100, result.Length);
        }

        [Test, Order(3)]
        public void Get_ShouldReturnAllVouchersByDefault()
        {
            for (var i = 0; i < 1000; i++)
            {
                _vouchers.Add(new Voucher
                {
                    Id = new Guid()
                });
            }

            var result = _controller.Get();

            Assert.AreEqual(_vouchers.Count, result.Length);
        }

        [Test, Order(1)]
        public void GetVouchersByName_ShouldReturnAllVouchersWithTheGivenName()
        {
            var a1Voucher = new Voucher { Id = new Guid(), Name = "A" };
            var a2Voucher = new Voucher { Id = new Guid(), Name = "A" };
            var b1Voucher = new Voucher { Id = new Guid(), Name = "B" };

            _vouchers.Add(a1Voucher);
            _vouchers.Add(a2Voucher);
            _vouchers.Add(b1Voucher);

            var result = _controller.GetVouchersByName("A");
            Assert.AreEqual(new[] { a1Voucher, a2Voucher }, result);
        }

        [Test, Order(2)]
        public void GetVouchersByNameSearch_ShouldReturnAllVouchersWhichMatchTheSearch()
        {
            var a1Voucher = new Voucher { Id = new Guid(), Name = "ABC" };
            var a2Voucher = new Voucher { Id = new Guid(), Name = "ABCD" };
            var b1Voucher = new Voucher { Id = new Guid(), Name = "ACD" };

            _vouchers.Add(a1Voucher);
            _vouchers.Add(a2Voucher);
            _vouchers.Add(b1Voucher);

            var result = _controller.GetVouchersByNameSearch("BC");
            Assert.AreEqual(new[] { a1Voucher, a2Voucher }, result);
        }
    }
}