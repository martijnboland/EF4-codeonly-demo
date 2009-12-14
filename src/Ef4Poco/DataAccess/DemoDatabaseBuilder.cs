using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.Objects;
using Ef4Poco.Domain;

namespace Ef4Poco.DataAccess
{
	public class DemoDatabaseBuilder
	{
		private IContextManager _contextManager;

		public DemoDatabaseBuilder(IContextManager contextManager)
		{
			_contextManager = contextManager;
		}

		public void Build()
		{
			using (var context = _contextManager.GetContext())
			{
				if (!context.DatabaseExists())
				{
					// Create database
					context.CreateDatabase();
					// Create reference data (1 teacher in our case)
					context.CreateObjectSet<Teacher>().AddObject(new Teacher() { Name = "A sample teacher" });
					context.SaveChanges();
				}
			}
		}
	}
}
