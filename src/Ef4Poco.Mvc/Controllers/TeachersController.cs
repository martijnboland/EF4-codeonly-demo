using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Ef4Poco.Domain;
using Ef4Poco.DataAccess;
using System.Transactions;

namespace Ef4Poco.Mvc.Controllers
{
	public class TeachersController : Controller
	{
		private ITeacherRepository _teacherRepository;

		public TeachersController(ITeacherRepository teacherRepository)
		{
			_teacherRepository = teacherRepository;
		}

		public ActionResult Index()
		{
			var teachers = _teacherRepository.Find().OrderBy(t => t.Name);
			return View(teachers);
		}

		public ActionResult Create()
		{
			return View();
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Create([Bind(Exclude="Id")] Teacher teacher)
		{
			if (ModelState.IsValid)
			{
				using (var ts = new TransactionScope())
				{
					_teacherRepository.AddNew(teacher);
					ts.Complete();
				}
				return RedirectToAction("Index");
			}
			return View();
		}

		public ActionResult Edit(int id)
		{
			var teacher = this._teacherRepository.FindOne(id);
			return View(teacher);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Edit(int id, FormCollection formData)
		{
			if (ModelState.IsValid)
			{
				using (var ts = new TransactionScope())
				{
					var teacher = _teacherRepository.FindOne(id);
					if (TryUpdateModel(teacher, new string[] { "Name" }))
					{
						this._teacherRepository.Update(teacher);
						ts.Complete();
						return RedirectToAction("Index");
					}
				}
			}
			return View();
		}
	}
}
