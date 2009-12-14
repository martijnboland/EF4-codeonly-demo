using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Ef4Poco.Domain
{
	public class Schedule
	{
		public virtual int Id { get; set; }

		[Required(ErrorMessage="Start date is required")]
		[DataType(DataType.Date, ErrorMessage="Invalid date")]
		public virtual DateTime StartDate { get; set; }
	
		[Required(ErrorMessage = "End date is required")]
		[DataType(DataType.Date, ErrorMessage = "Invalid date")]
		public virtual DateTime EndDate { get; set; }

		[Required(ErrorMessage = "Contact hours is required")]
		[StringLength(250, ErrorMessage = "Contact hours may not exceed {1} characters")]
		public virtual string ContactHours { get; set; }

		[StringLength(200, ErrorMessage="Location may not exceed {1} characters")]
		public virtual string Location { get; set; }
	
		public virtual Course Course { get; set; }
		public virtual Teacher Teacher { get; set; }
	}
}
