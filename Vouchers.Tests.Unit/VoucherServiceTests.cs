using Dominos.OLO.Vouchers.Models;
using Dominos.OLO.Vouchers.Repository.Interfaces;
using Dominos.OLO.Vouchers.Services;
using Dominos.OLO.Vouchers.Services.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominos.OLO.Vouchers.Tests.Unit
{
    [TestFixture]
    public class VoucherServiceTests
    {
        private readonly Mock<IVoucherRepository> mockedVoucherRepo = new Mock<IVoucherRepository>();
        private readonly Mock<ICouponService> mockedCouponService = new Mock<ICouponService>();
        private VoucherService _service;

        [SetUp]
        public void Setup()
        {
            _service = new VoucherService(mockedVoucherRepo.Object,
                                          mockedCouponService.Object);
        }

        private Voucher[] Prepare1000Vouchers()
        {
            IList<Voucher> vouchers = new List<Voucher>();

            for (var i = 0; i < 1000; i++)
            {
                vouchers.Add(new Voucher
                {
                    Id = new Guid(),
                    Price = 10
                });
            }

            return vouchers.ToArray();
        }

        [Test, Order(3)]
        public void Get_ShouldReturnRequestedNumberOfVouchers()
        {
            mockedVoucherRepo.Setup(x => x.GetVouchers())
                             .Returns(Prepare1000Vouchers);

            var result = _service.Get(100);

            Assert.AreEqual(100, result.Count());
        }

        [Test, Order(3)]
        public void Get_ShouldReturnRequestedNumberOfVouchersWithDiscount()
        {
            mockedVoucherRepo.Setup(x => x.GetVouchers())
                             .Returns(Prepare1000Vouchers);

            mockedCouponService.Setup(x => x.GetDiscount(It.IsAny<Guid>()))
                               .Returns(7);

            var result = _service.GetWithDiscount(It.IsAny<Guid>(), 100);

            Assert.AreEqual(100, result.Count());
            result.All(x => x.Price == 3);
        }

        [Test, Order(3)]
        public void Get_ShouldReturnRequestedVoucherWithDiscount()
        {
            Guid a_Id = new Guid();
            var a1Voucher = new Voucher { Id = a_Id, Name = "A" , Price = 8};
            var a2Voucher = new Voucher { Id = new Guid(), Name = "A", Price = 9 };
            var b1Voucher = new Voucher { Id = new Guid(), Name = "B", Price = 10 };

            IList<Voucher> vouchers = new List<Voucher>
            {
                    a1Voucher,a2Voucher,b1Voucher
            };

            mockedVoucherRepo.Setup(x => x.GetVouchers())
                             .Returns(vouchers.ToArray());

            mockedCouponService.Setup(x => x.GetDiscount(It.IsAny<Guid>()))
                               .Returns(3);

            var result = _service.GetVoucherByIdWithDiscount(a_Id, It.IsAny<Guid>());

            Assert.AreEqual(result.Price, 5);
        }

        [Test, Order(3)]
        public void Get_ShouldReturnAllVouchersByDefault()
        {
            var vouchers = Prepare1000Vouchers();

            mockedVoucherRepo.Setup(x => x.GetVouchers())
                             .Returns(vouchers);

            var result = _service.Get();

            Assert.AreEqual(vouchers.Length, result.Count());
        }

        [Test, Order(1)]
        public void GetVouchersByName_ShouldReturnAllVouchersWithTheGivenName()
        {
            var a1Voucher = new Voucher { Id = new Guid(), Name = "A" };
            var a2Voucher = new Voucher { Id = new Guid(), Name = "A" };
            var b1Voucher = new Voucher { Id = new Guid(), Name = "B" };

            IList<Voucher> vouchers = new List<Voucher>
            {
                a1Voucher,a2Voucher,b1Voucher
            };

            mockedVoucherRepo.Setup(x => x.GetVouchers())
                             .Returns(vouchers.ToArray());

            var result = _service.GetVouchersByName("A");

            Assert.AreEqual(new[] { a1Voucher, a2Voucher }, result);
        }

        [Test, Order(2)]
        public void GetVouchersByNameSearch_ShouldReturnAllVouchersWhichMatchTheSearch()
        {
            var a1Voucher = new Voucher { Id = new Guid(), Name = "ABC" };
            var a2Voucher = new Voucher { Id = new Guid(), Name = "ABCD" };
            var b1Voucher = new Voucher { Id = new Guid(), Name = "ACD" };

            IList<Voucher> vouchers = new List<Voucher>
            {
                a1Voucher,a2Voucher,b1Voucher
            };

            mockedVoucherRepo.Setup(x => x.GetVouchers())
                             .Returns(vouchers.ToArray());

            var result = _service.GetVouchersByNameSearch("BC");

            Assert.AreEqual(new[] { a1Voucher, a2Voucher }, result);
        }
    }
}