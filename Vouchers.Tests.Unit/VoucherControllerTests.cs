using Dominos.OLO.Vouchers.Controllers;
using Dominos.OLO.Vouchers.Models;
using Dominos.OLO.Vouchers.Services.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;

namespace Dominos.OLO.Vouchers.Tests.Unit
{
    [TestFixture]
    public class VoucherControllerTests
    {
        private readonly Mock<IVoucherService> mockedVoucherService = new Mock<IVoucherService>();
        private VoucherController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new VoucherController(mockedVoucherService.Object);
        }

        private Voucher[] Prepare1000Vouchers()
        {
            IList<Voucher> vouchers = new List<Voucher>();

            for (var i = 0; i < 1000; i++)
            {
                vouchers.Add(new Voucher
                {
                    Id = new Guid()
                });
            }

            return vouchers.ToArray();
        }

        [Test]
        public void Get_ShouldReturnRequestedNumberOfVouchers()
        {
            int count = 100;

            mockedVoucherService.Setup(x => x.Get(count))
                                .Returns(Prepare1000Vouchers().Take(count));

            var result = _controller.Get(count);

            Assert.AreEqual(count, result.Count());
        }

        [Test]
        public void Get_ShouldReturnRequestedVoucherById()
        {
            Guid a_Id = new Guid();
            var a1Voucher = new Voucher { Id = a_Id, Name = "A" };
            var a2Voucher = new Voucher { Id = new Guid(), Name = "A" };
            var b1Voucher = new Voucher { Id = new Guid(), Name = "B" };

            IList<Voucher> vouchers = new List<Voucher>
            {
                    a1Voucher,a2Voucher,b1Voucher
            };

            mockedVoucherService.Setup(x => x.GetVoucherById(a_Id))
                                .Returns(vouchers.First(x=> x.Id == a_Id));

            var result = _controller.GetVoucherById(a_Id);

            Assert.IsInstanceOf<OkNegotiatedContentResult<Voucher>>(result);
            Assert.AreEqual((result as OkNegotiatedContentResult<Voucher>).Content.Id, a_Id);

        }

        [Test]
        public void Get_ShouldReturnNotFoundWhenVoucherNotExists()
        {
            Guid a_Id = new Guid();

            mockedVoucherService.Setup(x => x.GetVoucherById(a_Id));

            var result = _controller.GetVoucherById(a_Id);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}
