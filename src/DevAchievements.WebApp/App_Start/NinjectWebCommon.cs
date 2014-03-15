using DevAchievements.Infrastructure.Repositories;
using Skahal.Infrastructure.Framework.Logging;
using DevAchievements.Infrastructure.Repositories.NHibernate;
using FluentNHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using DevAchievements.Infrastructure.Repositories.NHibernate.Mapping;
using FluentNHibernate.Cfg.Db;
using Skahal.Infrastructure.Framework.Repositories;
using Skahal.Infrastructure.Framework.Commons;
using DevAchievements.Domain;
using DevAchievements.Infrastructure.Web.Security;
using Skahal.Infrastructure.Framework.People;
using Skahal.Infrastructure.Framework.Domain;
using NHibernate;

[assembly: WebActivator.PreApplicationStartMethod(typeof(DevAchievements.WebApp.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(DevAchievements.WebApp.App_Start.NinjectWebCommon), "Stop")]

namespace DevAchievements.WebApp.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using NHibernate.Context;

    public static class NinjectWebCommon 
    {        
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();
        public static ISessionFactory s_sessionFactory;

        public static bool IsBind()
        {
            return CurrentSessionContext.HasBind(s_sessionFactory);
        }
        /// <summary>
        /// Gets the kernel.
        /// </summary>
        /// <value>The kernel.</value>
        public static IKernel Kernel {
            get {
                return bootstrapper.Kernel;
            }
        }
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
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            LogService.Debug ("Domain setup...");
            LogService.Debug("Registering NHibernate...");

            s_sessionFactory = Fluently.Configure()
                .Database(
                    MySQLConfiguration
                    .Standard
                    .Dialect<NHibernate.Dialect.MySQL5Dialect>()
                    .ConnectionString(a => a.FromConnectionStringWithKey("DevAchievements"))
                )
                .Mappings(m =>
                    m.FluentMappings.AddFromAssemblyOf<AuthenticationProviderUserMap>())
                .ExposeConfiguration((config) => {
                    //var schemaExport = new SchemaExport(config);
                    //schemaExport.Drop(false, true);
                    //schemaExport.Create(false, true);
                })
                .CurrentSessionContext<WebSessionContext>()
                .BuildSessionFactory();

            kernel.Bind<ISession>().ToMethod(
                (c) =>
                {
                    var session = s_sessionFactory.OpenSession();
                    CurrentSessionContext.Bind(session);

                    return session;
                }).InRequestScope()
            .OnActivation(a =>
            {
                LogService.Debug("ISession activated.");
            })
            .OnDeactivation(a =>
                {/*
                    if(CurrentSessionContext.HasBind(s_sessionFactory))
                    {
                        CurrentSessionContext.Unbind(s_sessionFactory);
                    }
                */
                    LogService.Debug("ISession deactivated.");
            });

            kernel.Bind<IUnitOfWork>().ToMethod(
                (c) => 
                new NHibernateUnitOfWork(c.Kernel.Get<ISession>())
            ).InRequestScope()
            .OnActivation (a =>
            {
                    LogService.Debug("IUnitOfWork activated.");
            })
            .OnDeactivation (a =>
            {
                    LogService.Debug("IUnitOfWork deactivated.");
            });

            BindNH<IAchievementRepository, NHibernateAchievementRepository, Achievement>(kernel);
            BindNH<IAchievementIssuerRepository, NHibernateAchievementIssuerRepository, AchievementIssuer>(kernel);
            BindNH<IDeveloperRepository, NHibernateDeveloperRepository, Developer>(kernel);
            BindNH<IAuthenticationProviderUserRepository, NHibernateAuthenticationProviderUserRepository, AuthenticationProviderUser>(kernel);

            LogService.Debug ("Registring repositories ...");
            DependencyService.Register<IUnitOfWork>(() => NinjectWebCommon.Kernel.Get<IUnitOfWork>());            
            DependencyService.Register<IAchievementRepository>(() => NinjectWebCommon.Kernel.Get<IAchievementRepository>());
            DependencyService.Register<IAchievementIssuerRepository>(() => NinjectWebCommon.Kernel.Get<IAchievementIssuerRepository>());
            DependencyService.Register<IDeveloperRepository>(() => NinjectWebCommon.Kernel.Get<IDeveloperRepository>());
            DependencyService.Register<IAuthenticationProviderUserRepository>(() => NinjectWebCommon.Kernel.Get<IAuthenticationProviderUserRepository>());
            DependencyService.Register<IUserRepository>(new MemoryUserRepository());
        }        

        private static void BindNH<TRepositoryInterface, TRepository, TEntity>(IKernel kernel)
            where TRepositoryInterface : IRepository<TEntity>
            where TRepository : TRepositoryInterface
            where TEntity : IAggregateRoot
        {
            kernel.Bind<TRepositoryInterface>().ToMethod((c) =>
                {
                    var repository = (TRepositoryInterface)Activator.CreateInstance(typeof(TRepository), c.Kernel.Get<ISession>());
                    repository.SetUnitOfWork(c.Kernel.Get<IUnitOfWork>());

                    return repository;
                }).InRequestScope();
        }
    }
}
