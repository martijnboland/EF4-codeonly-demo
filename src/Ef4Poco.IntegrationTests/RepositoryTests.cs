using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ef4Poco.DataAccess;
using System.Configuration;
using Ef4Poco.Domain;
using Microsoft.Data.Objects;

namespace Ef4Poco.IntegrationTests
{
	[TestClass]
	public class RepositoryTests
	{
		private IContextManager _contextManager;

		[TestInitialize]
		public void SetupContextManagerAndCreateDatabase()
		{
			_contextManager = new CoursesContextManager(ConfigurationManager.ConnectionStrings["CoursesConnection"].ConnectionString);
			var contextBuilder = new CoursesContextBuilder(ConfigurationManager.ConnectionStrings["CoursesConnection"].ConnectionString);
			using (var context = contextBuilder.GetContext())
			{
				if (context.DatabaseExists())
				{
					context.DeleteDatabase();
				}
				context.CreateDatabase();
			}
		}

		[TestMethod]
		public void RepositoryCreation()
		{
			IRepository<Course> courseRepository = new EfRepository<Course>(_contextManager);
			Assert.IsNotNull(courseRepository);
		}

		[TestMethod]
		public void RepositoryAddNew()
		{
			IRepository<Course> courseRepository = new EfRepository<Course>(_contextManager);
			courseRepository.AddNew(new Course() { Title = "Test course", Price = 900 });
			Assert.AreEqual(1, courseRepository.Find().Count());
		}

		[TestMethod]
		public void RepositoryFindOne()
		{
			IRepository<Course> courseRepository = new EfRepository<Course>(_contextManager);
			courseRepository.AddNew(new Course() { Title = "Test course", Price = 900 });

			var course = courseRepository.FindOne(1);
			Assert.IsNotNull(course);
		}

		[TestMethod]
		public void RepositoryQuery()
		{
			IRepository<Course> courseRepository = new EfRepository<Course>(_contextManager);
			courseRepository.AddNew(new Course() { Title = "Test course", Price = 900 });
			courseRepository.AddNew(new Course() { Title = "Test course 2", Price = 1158.56M });

			var courses = courseRepository.Find(c => c.Price > 1000).ToList();
			Assert.AreEqual(1, courses.Count);
		}

		[TestCleanup]
		public void CleanupContextManagerAndDatabase()
		{
			_contextManager.CleanupCurrent();
			var contextBuilder = new CoursesContextBuilder(ConfigurationManager.ConnectionStrings["CoursesConnection"].ConnectionString);
			using (var context = contextBuilder.GetContext())
			{
				if (context.DatabaseExists())
				{
					context.DeleteDatabase();
				}
			}
		}
	}
}
