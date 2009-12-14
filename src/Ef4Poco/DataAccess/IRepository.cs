using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Ef4Poco.DataAccess
{
	/// <summary>
	/// Generic data-access interface.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IRepository<T>
	{
		/// <summary>
		/// Find a single instance of T by primary key. 
		/// Note: we assume that primary keys are one column integers.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		T FindOne(int id);

		/// <summary>
		/// Find all instances of T and return it as IQueryable.
		/// </summary>
		/// <returns></returns>
        IQueryable<T> Find();


		/// <summary>
		/// Find instances of T for the given expression and return it as IQueryable.
		/// </summary>
		/// <param name="expression"></param>
		/// <returns></returns>
		IQueryable<T> Find(Expression<Func<T, bool>> expression);

		/// <summary>
		/// Save a new instance of T.
		/// </summary>
		/// <param name="item"></param>
		void AddNew(T item);

		/// <summary>
		/// Save an existing instance of T.
		/// </summary>
		/// <param name="item"></param>
		void Update(T item);

		/// <summary>
		/// Delete a given instance of T.
		/// </summary>
		/// <param name="item"></param>
		void Delete(T item);
	}
}
