using System;
using CoworkersTotalizator.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace CoworkersTotalizator.Filters
{
	public class Authorization : Attribute, IActionFilter
	{
		private LoginService _loginService;

		private bool IsAdminOnly { get; }

		public Authorization(LoginService loginService, bool IsAdminOnly)
		{
			this._loginService = loginService;
			this.IsAdminOnly = IsAdminOnly;
		}

		public void OnActionExecuting(ActionExecutingContext context)
		{
			if(context.HttpContext.Request.Path == "/api/login/login")
			{
				this._loginService.Validate(Guid.NewGuid());
			}
		}

		public void OnActionExecuted(ActionExecutedContext context)
		{
			
		}
	}

	public class AuthorizationMetadata : Attribute, IFilterFactory
	{
		private bool isAdminOnly;

		public AuthorizationMetadata(bool isAdminOnly = false)
		{
			this.isAdminOnly = isAdminOnly;
		}

		public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
		{
			return new Authorization(
				serviceProvider.GetService<LoginService>(),
				this.isAdminOnly);
		}

		public bool IsReusable => false;
	}
}
