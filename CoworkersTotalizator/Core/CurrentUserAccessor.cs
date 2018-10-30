using System;
using System.Linq;
using CoworkersTotalizator.Dal;
using CoworkersTotalizator.Models.Users;
using CoworkersTotalizator.Services;
using Microsoft.AspNetCore.Http;

namespace CoworkersTotalizator.Core
{
	public class CurrentUserAccessor : ICurrentUserAccessor
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly CoworkersTotalizatorContext _context;

		public CurrentUserAccessor(
			IHttpContextAccessor httpContextAccessor,
			CoworkersTotalizatorContext context)
		{
			this._httpContextAccessor = httpContextAccessor;
			this._context = context;
		}

		public User GetCurrentUser()
		{
			var req = this._httpContextAccessor.HttpContext.Request;
			var authHeader = req.Headers["Authorization"].First();
			if (Guid.TryParse(authHeader, out var token))
			{
				var tokenHist = this._context.TokenHistory.FirstOrDefault(x => x.Id == token);
				if (tokenHist == null)
				{
					return null;
				}

				return this._context.Users.First(x => x.Name == tokenHist.UserId);
			}

			return null;
		}
	}
}
