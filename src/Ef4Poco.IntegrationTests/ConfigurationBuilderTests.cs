using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ef4Poco.DataAccess;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Data.Objects;
using System.Data.Common;

namespace Ef4Poco.IntegrationTests
{
	[TestClass]
	public class ConfigurationBuilderTests
	{
		[TestMethod]
		public void CreateContext()
		{
			var builder = new CoursesContextBuilder(ConfigurationManager.ConnectionStrings["CoursesConnection"].ConnectionString);
			var context = builder.GetContext();
			Assert.IsNotNull(context);
		}

		[TestMethod]
		public void CreateDatabase()
		{
			var builder = new CoursesContextBuilder(ConfigurationManager.ConnectionStrings["CoursesConnection"].ConnectionString);
			var context = builder.GetContext();
			if (context.DatabaseExists())
			{
				context.DeleteDatabase();
			}
			context.CreateDatabase();
			Assert.IsTrue(context.DatabaseExists());
			
		}
	}
}
