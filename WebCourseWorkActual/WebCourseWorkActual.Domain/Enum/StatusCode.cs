using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCourseWorkActual.Domain.Enum
{
	public enum StatusCode
	{
		UserNotFound = 0,
		UserAlreadyExists = 1,
		ExpenseCategoryAlreadyExists = 2,
		OK = 200,
		InternalServerError = 500
	}
}
