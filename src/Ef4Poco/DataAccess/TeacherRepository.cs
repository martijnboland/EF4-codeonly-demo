using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ef4Poco.Domain;

namespace Ef4Poco.DataAccess
{
	public class TeacherRepository : EfRepository<Teacher>, ITeacherRepository
	{
		public TeacherRepository(IContextManager contextManager) : base(contextManager)
		{ }
	}
}
