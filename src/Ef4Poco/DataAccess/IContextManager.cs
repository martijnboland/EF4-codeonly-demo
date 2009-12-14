using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace Ef4Poco.DataAccess
{
	public interface IContextManager
	{
		/// <summary>
		/// Get an ObjectContext. Implementors have to decide the lifestyle and where to store the object context.
		/// </summary>
		/// <returns></returns>
		ObjectContext GetContext();

		/// <summary>
		/// Finalizes and cleans the ObjectContect for the current user context.
		/// </summary>
		void CleanupCurrent();
	}
}
