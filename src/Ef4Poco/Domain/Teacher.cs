using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Ef4Poco.Domain
{
	public class Teacher
	{
		public virtual int Id { get; set; }

		[Required(ErrorMessage="Teacher name is required")]
		[StringLength(50, ErrorMessage="Teacher name must not exceed {1} characters")]
		public virtual string Name { get; set; }
	}
}
