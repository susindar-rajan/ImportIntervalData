using ReadExcelToDb.Interfaces;
using ReadExcelToDb.Services;
using System;
using System.Linq;
using System.Web.Mvc;
using Unity;
using Unity.Lifetime;
using Unity.Mvc5;

namespace ReadExcelToDb
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IExcelRepository, ExcelRepository>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}