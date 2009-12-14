using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Web;
using System.Runtime.Remoting.Messaging;

namespace Ef4Poco.DataAccess
{
	/// <summary>
	/// Manages and stores the ObjectContext for the current user context.
	/// </summary>
	public class CoursesContextManager : IContextManager
	{
		private const string ContextKey = "CURRENT_OBJECTCONTEXT";
		private CoursesContextBuilder _contextBuilder;

		public CoursesContextManager(string connectionString)
		{
			this._contextBuilder = new CoursesContextBuilder(connectionString);
		}

		public ObjectContext GetContext()
		{
			if (IsInWebContext())
			{
				if (HttpContext.Current.Items[ContextKey] == null)
				{
					HttpContext.Current.Items[ContextKey] = _contextBuilder.GetContext();
				}
				return (ObjectContext)HttpContext.Current.Items[ContextKey];
			}
			else
			{
				if (CallContext.GetData(ContextKey) == null)
				{
					CallContext.SetData(ContextKey, _contextBuilder.GetContext());
				}
				return (ObjectContext)CallContext.GetData(ContextKey);
			}
		}

		public void CleanupCurrent()
		{
			ObjectContext objectContext;
			if (IsInWebContext())
			{
				objectContext = HttpContext.Current.Items[ContextKey] as ObjectContext;
				HttpContext.Current.Items.Remove(ContextKey);
			}
			else
			{
				objectContext = CallContext.GetData(ContextKey) as ObjectContext;
				CallContext.FreeNamedDataSlot(ContextKey);
			}
			if (objectContext != null)
			{
				objectContext.Dispose();
			}
		}

		private bool IsInWebContext()
		{
			return HttpContext.Current != null;
		}
	}
}
