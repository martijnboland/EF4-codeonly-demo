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
    public class CoursesController : Controller
    {
		private ICourseRepository _courseRepository;
		private ITeacherRepository _teacherRepository;

		public CoursesController(ICourseRepository courseRepository, ITeacherRepository teacherRepository)
		{
			_courseRepository = courseRepository;
			_teacherRepository = teacherRepository;
		}

        public ActionResult Index()
        {
			var courses = _courseRepository.Find();
            return View(courses);
        }

		public ActionResult Create()
		{
			return View(new Course());
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Create([Bind(Exclude="Id")]Course course)
		{
			if (ModelState.IsValid)
			{
				using (var ts = new TransactionScope())
				{
					_courseRepository.AddNew(course);
					ts.Complete();
				}
				return RedirectToAction("Index");
			}
			return View(course);
		}

		public ActionResult Edit(int id)
		{
			var course = _courseRepository.FindOne(id);
			return View(course);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Edit(int id, FormCollection collection)
		{
			var course = _courseRepository.FindOne(id);
			if (TryUpdateModel(course, new string[] { "Title", "Price" }) && ModelState.IsValid)
			{
				using (var ts = new TransactionScope())
				{
					_courseRepository.Update(course);
					ts.Complete();
				}
				return RedirectToAction("Index");
			}
			return View(course);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Delete(int id)
		{
			using (var ts = new TransactionScope())
			{
				var course = _courseRepository.FindOne(id);
				this._courseRepository.DeleteCourseWithSchedule(course);
				ts.Complete();
			}
			return RedirectToAction("Index");
		}

		public ActionResult Schedule(int courseId)
		{
			var course = _courseRepository.GetCourseWithSchedulesAndTeachers(courseId);
			return View(course);
		}

		public ActionResult CreateSchedule(int courseId)
		{
			var course = _courseRepository.FindOne(courseId);
			var teachers = _teacherRepository.Find();
			ViewData["Teachers"] = new SelectList(teachers, "Id", "Name");
			return View(new Schedule { Course = course });
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult CreateSchedule(int courseId, int teacherId, [Bind(Exclude="Id")]Schedule schedule)
		{
			var course = _courseRepository.FindOne(courseId);
			var teacher = _teacherRepository.FindOne(teacherId);
			schedule.Course = course;
			schedule.Teacher = teacher;

			if (ModelState.IsValid)
			{
				using (var ts = new TransactionScope())
				{
					course.Schedules.Add(schedule);
					this._courseRepository.Update(course);
					ts.Complete();
				}
				return RedirectToAction("Schedule", new { courseid = courseId });
			}
			var teachers = _teacherRepository.Find();
			ViewData["Teachers"] = new SelectList(teachers, "Id", "Name", teacherId);
			return View(schedule);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult DeleteSchedule(int courseId, int id)
		{
			using (var ts = new TransactionScope())
			{
				var course = _courseRepository.GetCourseWithSchedulesAndTeachers(courseId);
				var schedule = course.Schedules.Single(s => s.Id == id);
				_courseRepository.RemoveScheduleFromCourse(schedule, course);
				ts.Complete();
			}
			return RedirectToAction("Schedule", new { courseid = courseId });
		}
    }
}
