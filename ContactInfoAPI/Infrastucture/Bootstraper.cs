using ContactInfo.Data.Interfaces;
using ContactInfo.Data.Repository;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity;
using Unity.WebApi;
using Unity.Lifetime;
using System.Web.Http;
using System.Data.Entity;
using ContactInfo.Data.Models;
using Unity.Injection;

namespace ContactInfoAPI.Infrastucture
{
    public class Bootstraper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();  
            container.RegisterType<DbContext, ContactInfoEntities>(new InjectionConstructor());
            container.RegisterType(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            container.RegisterType<IContactRepository, ContactRepositroy>();
         

            return container;



        }
    }
}