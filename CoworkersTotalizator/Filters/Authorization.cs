using System;
using CoworkersTotalizator.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace CoworkersTotalizator.Filters
{
	public class Authorization : Attribute, IActionFilter
	{
		private readonly LoginService _loginService;
		private readonly IHostingEnvironment _env;

		private bool IsAdminOnly { get; }

		public Authorization(LoginService loginService, IHostingEnvironment env, bool isAdminOnly)
		{
			this._loginService = loginService;
			this._env = env;
			this.IsAdminOnly = isAdminOnly;
		}

		public void OnActionExecuting(ActionExecutingContext context)
		{
			if (_env.IsProduction())
			{
				if (context.HttpContext.Request.Path != "/api/login/login")
				{
					if (!(Guid.TryParse(context.HttpContext.Request.Headers["Authorization"], out var token) &&
						  this._loginService.Validate(token, IsAdminOnly)))
					{
						context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
					}
				}
			}
		}

		public void OnActionExecuted(ActionExecutedContext context)
		{

		}
	}

	public class AuthorizationMetadata : Attribute, IFilterFactory
	{
		private readonly bool _isAdminOnly;

		public AuthorizationMetadata(bool isAdminOnly = false)
		{
			this._isAdminOnly = isAdminOnly;
		}

		public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
		{
			return new Authorization(
				serviceProvider.GetService<LoginService>(),
				serviceProvider.GetService<IHostingEnvironment>(),
				this._isAdminOnly);
		}

		public bool IsReusable => false;
	}
}
