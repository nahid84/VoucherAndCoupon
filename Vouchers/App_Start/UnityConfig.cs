using Dominos.OLO.Vouchers.Repository;
using Dominos.OLO.Vouchers.Repository.Interfaces;
using Dominos.OLO.Vouchers.Services;
using Dominos.OLO.Vouchers.Services.Interfaces;
using System;
using System.Web.Http;
using Unity;
using Unity.Injection;
using Unity.WebApi;

namespace Dominos.OLO.Vouchers
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IVoucherRepository, VoucherRepository>
                (new InjectionConstructor($"{ AppDomain.CurrentDomain.BaseDirectory }data.json"));
            container.RegisterType<ICouponRepository, CouponRepository>
                (new InjectionConstructor($"{ AppDomain.CurrentDomain.BaseDirectory }coupon-data.json"));
            container.RegisterType<IVoucherService, VoucherService>();
            container.RegisterType<ICouponService, CouponService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}