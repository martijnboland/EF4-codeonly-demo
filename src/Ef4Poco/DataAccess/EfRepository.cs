using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Data.Objects;
using System.Data;
using Microsoft.Data.Objects;

namespace Ef4Poco.DataAccess
{
	public class EfRepository<T> : IRepository<T> where T : class
	{
		private IContextManager _contextManager;
		private string _qualifiedEntitySetName;
		private string _keyName;

		protected ObjectContext CurrentObjectContext
		{
			get { return _contextManager.GetContext(); }
		}

		protected ObjectSet<T> ObjectSet
		{
			get { return CurrentObjectContext.CreateObjectSet<T>(); }
		}

		/// <summary>
		/// Creates a new instance of the <see cref="EfRepository<T>"/> class.
		/// </summary>
		/// <param name="objectContext"></param>
		public EfRepository(IContextManager contextManager)
		{
			this._contextManager = contextManager;
			this._qualifiedEntitySetName = string.Format("{0}.{1}"
				, this.ObjectSet.EntitySet.EntityContainer.Name
				, this.ObjectSet.EntitySet.Name);
			this._keyName = this.ObjectSet.EntitySet.ElementType.KeyMembers.Single().Name;
		}

		public T FindOne(int id)
		{			
			object value;
			var entityKey = new EntityKey(this._qualifiedEntitySetName, this._keyName, id);
			if (this.CurrentObjectContext.TryGetObjectByKey(entityKey, out value))
			{
				return (T)value;
			}
			return null;
		}

		public IQueryable<T> Find()
		{
			return ObjectSet;
		}

		public IQueryable<T> Find(Expression<Func<T, bool>> expression)
		{
			return ObjectSet.Where(expression);
		}

		public void AddNew(T item)
		{
			ObjectSet.AddObject(item);
			CurrentObjectContext.SaveChanges();
		}

		public void Update(T item)
		{
			CurrentObjectContext.SaveChanges();
			// TODO: how to handle detached objects? Or how do we KNOW that an entity isn't already in the context without
			// having to query for it.
		}

		public void Delete(T item)
		{
			ObjectSet.DeleteObject(item);
			CurrentObjectContext.SaveChanges();
		}
	}
}
