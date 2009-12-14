using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.Objects;
using System.Data.Objects;
using System.Data.Common;
using Ef4Poco.Domain;
using Ef4Poco.DataAccess;
using System.Configuration;
using System.Data.SqlClient;

namespace Ef4Poco.DataAccess
{
	public class CoursesContextBuilder
	{
		private ContextBuilder<ObjectContext> _builder;
		private string _defaultConnectionString;

		public CoursesContextBuilder(string defaultConnectionString)
		{
			this._defaultConnectionString = defaultConnectionString;
			this._builder = new ContextBuilder<ObjectContext>();
			ConfigureMappings();
		}

		private void ConfigureMappings()
		{
            _builder.Configurations.Add(new TeacherMapping());
            _builder.Configurations.Add(new CourseMapping());
            _builder.Configurations.Add(new ScheduleMapping());
		}

		public ObjectContext GetContext()
		{
			return GetContext(_defaultConnectionString);
		}

		public ObjectContext GetContext(string connectionString)
		{
			var context = _builder.Create(GetConnection(connectionString));
			context.ContextOptions.LazyLoadingEnabled = true;
			return context;
		}

		private DbConnection GetConnection(string connectionString)
		{
			// Hardcoded to SqlConnection for this demo.
			return new SqlConnection(connectionString);
		}
	}
}
