using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;
using Ef4Poco.Mvc.Infrastructure;
using Castle.MicroKernel.Registration;
using System.Reflection;
using Ef4Poco.DataAccess;
using System.Configuration;

namespace Ef4Poco.Mvc
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		private IWindsorContainer _container;

		protected void Application_Start()
		{
			InitializeContainer();
			RegisterRoutes(RouteTable.Routes);
			InitializeDatabase();
		}

		protected void Application_End()
		{
			this._container.Dispose();
		}

		protected void Application_EndRequest()
		{
			if (_container != null)
			{
				var contextManager = _container.Resolve<IContextManager>();
				contextManager.CleanupCurrent();
			}
		}

		private void InitializeContainer()
		{
			_container = new WindsorContainer();

			ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(_container));

			// Register context manager.
			_container.Register(
				Component.For<IContextManager>()
				.ImplementedBy<CoursesContextManager>()
				.LifeStyle.Singleton
				.Parameters(
	                Parameter.ForKey("connectionString").Eq(ConfigurationManager.ConnectionStrings["CoursesConnection"].ConnectionString)	
				)
			);
			// Register specifc repository implementations (can we do this more generic?)
			_container.Register(
				Component.For<ICourseRepository>()
				.ImplementedBy<CourseRepository>()
				.LifeStyle.Singleton
			);
			_container.Register(
				Component.For<ITeacherRepository>()
				.ImplementedBy<TeacherRepository>()
				.LifeStyle.Singleton
			);

			// Register database builder
			_container.Register(Component.For<DemoDatabaseBuilder>());
			
			// Register all MVC controllers
			_container.Register(AllTypes.Of<IController>()
				.FromAssembly(Assembly.GetExecutingAssembly())
				.Configure(c => c.LifeStyle.Transient)
			);
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

			routes.MapRoute(
				"Default",                                              // Route name
				"{controller}/{action}/{id}",                           // URL with parameters
				new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
			);
		}

		private void InitializeDatabase()
		{
			var databaseBuilder = _container.Resolve<DemoDatabaseBuilder>();
			databaseBuilder.Build();
		}
	}
}