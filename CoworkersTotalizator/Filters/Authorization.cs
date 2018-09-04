using System;
using CoworkersTotalizator.Dal;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace CoworkersTotalizator.Filters
{
	public class Authorization : Attribute, IActionFilter, IFilterFactory
	{
		private CoworkersTotalizatorContext _context;

		private bool IsAdmin { get; }

		public Authorization(bool isAdmin = false)
		{
			this.IsAdmin = isAdmin;
		}

		private Authorization(CoworkersTotalizatorContext context, bool isAdmin)
		{
			this._context = context;
			this.IsAdmin = isAdmin;
		}

		public void OnActionExecuting(ActionExecutingContext context)
		{

		}

		public void OnActionExecuted(ActionExecutedContext context)
		{
			throw new NotImplementedException();
		}

		public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
		{
			return new Authorization(
				serviceProvider.GetRequiredService<CoworkersTotalizatorContext>(),
				this.IsAdmin);
		}

		public bool IsReusable => false;
	}
}
