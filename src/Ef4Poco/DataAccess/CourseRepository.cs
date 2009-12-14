using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ef4Poco.Domain;

namespace Ef4Poco.DataAccess
{
	/// <summary>
	/// Course-specific repository.
	/// </summary>
	public class CourseRepository : EfRepository<Course>, ICourseRepository
	{
		public CourseRepository(IContextManager contextManager)
			: base(contextManager)
		{ }

		public Course GetCourseWithSchedulesAndTeachers(int courseId)
		{
			// Wouldn't it be nice to have strong-typed includes?
			var query = from c in ObjectSet.Include("Schedules").Include("Schedules.Teacher")
						where c.Id == courseId
						select c;
			return query.Single();
		}

		public void RemoveScheduleFromCourse(Schedule schedule, Course course)
		{
			course.Schedules.Remove(schedule);
			CurrentObjectContext.DeleteObject(schedule);
			CurrentObjectContext.SaveChanges();
		}

		public void DeleteCourseWithSchedule(Course course)
		{
			// Howto configure cascade delete via code? This is a little cumbersome.
			var schedules = new List<Schedule>(course.Schedules);
			foreach (var schedule in schedules)
			{
				CurrentObjectContext.DeleteObject(schedule);
			}
			CurrentObjectContext.DeleteObject(course);
			CurrentObjectContext.SaveChanges();
		}
	}
}
