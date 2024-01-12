using System.Web.Http;
using Unity;
using Unity.AspNet.WebApi;
using BUS.Interfaces;
using BUS;
using GUI.Controllers.Api;
using DAL.Interfaces;
using DAL;


namespace GUI.App_Start
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<IProvinceDal, ProvinceDal>();
            container.RegisterType<IEmployeeDal, EmployeeDal>();
            container.RegisterType<IDistrictDal, DistrictDal>();
            container.RegisterType<ITownDal, TownDal>();
            container.RegisterType<IQualificationDal, QualificationDal>();

            container.RegisterType<IProvinceBus, ProvinceBus>();
            container.RegisterType<IEmployeeBus, EmployeeBus>();
            container.RegisterType<IDistrictBus, DistrictBus>();
            container.RegisterType<ITownBus, TownBus>();
            container.RegisterType<IQualificationBus, QualificationBus>();

            container.RegisterType(typeof(IBaseDal<>), typeof(BaseDal<>));


            // Register your controllers, including TestApiController
            container.RegisterType<GetApiController>();

            // Set the UnityDependencyResolver for MVC
            System.Web.Mvc.DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));

            // Set the UnityDependencyResolver for Web API
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}