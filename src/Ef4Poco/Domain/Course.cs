using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Ef4Poco.Domain
{
	public class Course
	{
		public virtual int Id { get; set; }

		[Required(ErrorMessage="Course title is required")]
		public virtual string Title { get; set; }
	
		[Required(ErrorMessage = "Price is required")]
		[DataType(DataType.Currency)]
		[Range(10.00, double.MaxValue, ErrorMessage="The minimum price is {1}")]
		public virtual decimal Price { get; set; }

		public virtual ISet<Schedule> Schedules { get; set; }

		public Course()
		{
			this.Schedules = new HashSet<Schedule>();
		}
	}
}
