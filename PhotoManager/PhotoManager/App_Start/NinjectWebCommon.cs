using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using Ninject.Web.Common;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject.Web.Common.WebHost;
using PhotoManager.DAL.Interfaces;
using PhotoManager.DAL.Repositories;
using System.Data.Entity;
using PhotoManager.DAL.EF;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(PhotoManager.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(PhotoManager.App_Start.NinjectWebCommon), "Stop")]

namespace PhotoManager.App_Start
{
    public class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();
  
        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        ///// <summary>
        ///// Load your modules or register your services here!
        ///// </summary>
        ///// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            //////System.Web.Mvc.DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));

            //kernel.Bind<DataContextProvider>().ToSelf().InSingletonScope();

            kernel.Bind(typeof(DbContext)).To(typeof(PhotoManagerContext)).InSingletonScope();

            kernel.Bind(typeof(IRepository<>)).To(typeof(GenericRepository<>)).InSingletonScope();

            //kernel.Bind<TestingResultCsvMapper>().ToSelf();

            //kernel.Bind<IAdvancedLogicService>().To<AdvancedLogicService>().InRequestScope();
            //kernel.Bind<IGetInfoService>().To<GetInfoService>().InRequestScope();
            //kernel.Bind<ILowLevelTestManagementService>().To<LowLevelLowLevelTestManagementService>().InRequestScope();
            //kernel.Bind<IHighLevelTestManagementService>().To<HighLevelTestManagementService>().InRequestScope();

            //kernel.Bind<IUserService>().To<UserService>().InRequestScope();

            //kernel.Bind<IAdvancedMapper>().To<AdvancedMapper>().InRequestScope();
            //kernel.Bind<IMapper>().ToConstant(AutoMapperConfiguration.MapperInstance);
        }
    }
}
