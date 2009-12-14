using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.Objects;
using Ef4Poco.Domain;

namespace Ef4Poco.DataAccess
{
	public class TeacherMapping : EntityConfiguration<Teacher>
	{
		public TeacherMapping()
		{
			HasKey(t => t.Id);
			Property(t => t.Id).IsIdentity();
			MapSingleType(t => new
			{
				teacherid = t.Id,
				name = t.Name
			}).ToTable("teacher");
		}
	}

	public class CourseMapping : EntityConfiguration<Course>
	{
		public CourseMapping()
		{
			HasKey(c => c.Id);
			Property(c => c.Id).IsIdentity();
			MapSingleType(c => new
			{
				courseid = c.Id,
				title = c.Title,
				price = c.Price
			}).ToTable("course");
			Property(c => c.Price).HasStoreType("money").HasPrecision(19, 4);
		}
	}

	public class ScheduleMapping : EntityConfiguration<Schedule>
	{
		public ScheduleMapping()
		{
			HasKey(s => s.Id);
			Property(s => s.Id).IsIdentity();
			MapSingleType(s => new
			{
				scheduleid = s.Id,
				startdate = s.StartDate,
				enddate = s.EndDate,
				contacthours = s.ContactHours,
				location = s.Location,
				teacherid = s.Teacher.Id,
				courseid = s.Course.Id
			}).ToTable("schedule");
			// Inverse mapping to Course.Schedules
			Relationship(s => s.Course).FromProperty(c => c.Schedules).IsRequired();
		}
	}
}
