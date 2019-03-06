using System;
using System.Data.Entity;
using System.Web;
using AutoMapper;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;
using PhotoManager.Controllers;
using PhotoManager.DAL.EF;
using PhotoManager.DAL.Interfaces;
using PhotoManager.DAL.Repositories;
using PhotoManager.Model.ViewModels.Mapping;
using Services;
using Services.Interfaces;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace PhotoManager.Controllers
{
    public static class NinjectWebCommon 
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

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind(typeof(DbContext)).To(typeof(PhotoManagerContext)).InSingletonScope();
            kernel.Bind(typeof (IRepository<>)).To(typeof (GenericRepository<>)).InRequestScope();
            kernel.Bind<IPhotoRepository>().To<PhotoRepository>().InRequestScope();

            kernel.Bind<IPhotoGetInfoService>().To<PhotoGetInfoService>().InRequestScope();
            kernel.Bind<IPhotoManagementService>().To<PhotoManagementService>().InRequestScope();

            kernel.Bind<IAlbumGetInfoService>().To<AlbumGetInfoService>().InRequestScope();
            kernel.Bind<IAlbumManagementService>().To<AlbumManagementService>().InRequestScope();

            kernel.Bind<IUserService>().To<UserService>().InRequestScope();
            kernel.Bind<IAdvancedMapper>().To<AdvancedMapper>().InRequestScope();
            kernel.Bind<IMapper>().ToConstant(AutoMapperConfiguration.MapperInstance);
        }
    }
}
