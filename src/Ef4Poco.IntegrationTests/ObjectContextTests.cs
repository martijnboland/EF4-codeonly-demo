using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ef4Poco.DataAccess;
using System.Configuration;
using Microsoft.Data.Objects;
using Ef4Poco.Domain;

namespace Ef4Poco.IntegrationTests
{
	[TestClass]
	public class ObjectContextTests
	{
		private CoursesContextBuilder _contextBuilder;

		[TestInitialize]
		public void SetupDatabaseAndContextBuilder()
		{
			_contextBuilder = new CoursesContextBuilder(ConfigurationManager.ConnectionStrings["CoursesConnection"].ConnectionString);
			using (var context = _contextBuilder.GetContext())
			{
				if (context.DatabaseExists())
				{
					context.DeleteDatabase();
				}
				context.CreateDatabase();
			}
		}

		[TestMethod]
		public void CanCreateObjectset()
		{
			using (var context = _contextBuilder.GetContext())
			{
				var courses = context.CreateObjectSet<Course>();
				Assert.IsNotNull(courses);
			}
		}

		[TestMethod]
		public void InsertObject()
		{
			using (var context = _contextBuilder.GetContext())
			{
				Course course = new Course() { Title = "Test course", Price = 59.95M };
				var coursesSet = context.CreateObjectSet<Course>();
				coursesSet.AddObject(course);
				context.SaveChanges();
			}

			using (var context = _contextBuilder.GetContext())
			{
				var query = from c in context.CreateObjectSet<Course>()
							select c;
				Assert.AreEqual(1, query.Count());
				var course = query.First();
				Assert.AreEqual(59.95M, course.Price);
			}
		}

		[TestMethod]
		public void InsertObjectGraph()
		{
			using (var context = _contextBuilder.GetContext())
			{
				Course course = new Course() { Title = "Test course", Price = 59.95M };
				var schedule = new Schedule() { Course = course, StartDate = new DateTime(2009, 1, 1), EndDate = new DateTime(2009, 12, 31), Location = "Taiga HQ" };
				schedule.Teacher = new Teacher() { Name = "Teacher1" };
				course.Schedules.Add(schedule);
				
				var coursesSet = context.CreateObjectSet<Course>();
				coursesSet.AddObject(course);
				context.SaveChanges();
			}

			using (var context = _contextBuilder.GetContext())
			{
				var query = from c in context.CreateObjectSet<Course>()
							select c;
				Assert.AreEqual(1, query.Count());
				var course = query.First();
				Assert.AreEqual(1, course.Schedules.Count);
				var schedule = course.Schedules.First();
				Assert.AreEqual(new DateTime(2009, 1, 1), schedule.StartDate);
				Assert.AreEqual("Teacher1", schedule.Teacher.Name);
			}
		}

		[TestMethod]
		public void UpdateObjectGraph()
		{
			// Setup initial object graph
			using (var context = _contextBuilder.GetContext())
			{
				Course course = new Course() { Title = "Test course", Price = 59.95M };
				var schedule = new Schedule() { Course = course, StartDate = new DateTime(2009, 1, 1), EndDate = new DateTime(2009, 12, 31), Location = "Taiga HQ" };
				schedule.Teacher = new Teacher() { Name = "Teacher1" };
				course.Schedules.Add(schedule);

				var coursesSet = context.CreateObjectSet<Course>();
				coursesSet.AddObject(course);
				context.SaveChanges();
			}

			// Update object graph within a new context.
			using (var context = _contextBuilder.GetContext())
			{
				var course = context.CreateObjectSet<Course>().First();

				// Change property value
				course.Title = "Test course (updated)";
				
				// Add a new schedule and teacher to the course
				var schedule = new Schedule() { Course = course, StartDate = new DateTime(2010, 1, 1), EndDate = new DateTime(2010, 12, 31), Location = "Taiga HQ" };
				schedule.Teacher = new Teacher() { Name = "Teacher2" };
				course.Schedules.Add(schedule);

				// Remove existing schedule from the course
				var scheduleToRemove = course.Schedules.Where(s => s.StartDate == new DateTime(2009, 1, 1)).Single();
				course.Schedules.Remove(scheduleToRemove);
				context.DeleteObject(scheduleToRemove);

				context.SaveChanges();
			}

			// Check.
			using (var context = _contextBuilder.GetContext())
			{
				var query = from c in context.CreateObjectSet<Course>()
							select c;
				Assert.AreEqual(1, query.Count());
				var course = query.First();
				Assert.AreEqual("Test course (updated)", course.Title);
				Assert.AreEqual(1, course.Schedules.Count);
				var schedule = course.Schedules.First();
				Assert.AreEqual(new DateTime(2010, 1, 1), schedule.StartDate);
				Assert.AreEqual("Teacher2", schedule.Teacher.Name);
				Assert.AreEqual(2, context.CreateObjectSet<Teacher>().Count());
			}
		}

		[TestCleanup]
		public void Cleanup()
		{
			using (var context = _contextBuilder.GetContext())
			{
				if (context.DatabaseExists())
				{
					context.DeleteDatabase();
				}
			}
		}
	}
}
