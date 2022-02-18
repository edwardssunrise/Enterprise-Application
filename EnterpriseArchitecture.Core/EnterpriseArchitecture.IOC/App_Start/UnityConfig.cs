using System.Web.Mvc;
using EntepriseArchitecture.Data.GenericRepository;
using EntepriseArchitecture.Data.UnitOfWork;
using EnterpriseArchitecture.Core;
using EnterpriseArchitecture.Services.Abstract;
using EnterpriseArchitecture.Services.Concrete;
using Microsoft.Practices.Unity;
using Unity.Mvc5;

namespace EnterpriseArchitecture.IOC
{
    /// <summary>
    /// IoC(Inversion Of Control) is a software development principle that aims to create 
    /// objects with loose coupling throughout the lifecycle of the application. It is 
    /// responsible for the life cycle of the objects and provides its management. When 
    /// an interface is injected into the class using IoC, the related interface methods 
    /// become available. Thus, the class using IoC only knows the methods it will use, 
    /// even if there are more methods in the class, it will be able to access the methods 
    /// specified in the interface.
    /// </summary>
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            RegisterTypes(container);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.BindInRequestScope<IUnitOfWork, UnitOfWork>();
            
            container.BindInRequestScope<IUserService, UserService>();
            container.BindInRequestScope<IPostService, PostService>();
            container.BindInRequestScope<ICategoryService, CategoryService>();

            container.BindInRequestScope<IGenericRepository<User>, GenericRepository<User>>();
            container.BindInRequestScope<IGenericRepository<Post>, GenericRepository<Post>>();
            container.BindInRequestScope<IGenericRepository<Category>, GenericRepository<Category>>();
        }

        /// <summary>
        /// The following class is used to bind to the Classes that the Interface is connected to.
        /// </summary>
        public static void BindInRequestScope<T1, T2>(this IUnityContainer container) where T2 : T1
        {
            container.RegisterType<T1, T2>(new HierarchicalLifetimeManager());
        }
    }
}