using System;
using Ef4Poco.Domain;
namespace Ef4Poco.DataAccess
{
	public interface ICourseRepository : IRepository<Course>
	{
		void DeleteCourseWithSchedule(Course course);
		Ef4Poco.Domain.Course GetCourseWithSchedulesAndTeachers(int courseId);
		void RemoveScheduleFromCourse(Schedule schedule, Course course);
	}
}
