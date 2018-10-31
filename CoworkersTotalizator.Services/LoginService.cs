using System;
using System.Linq;
using CoworkersTotalizator.Dal;
using CoworkersTotalizator.Models.Users;
using MailKit.Net.Smtp;
using MimeKit;

namespace CoworkersTotalizator.Services
{
	public class LoginService
	{
		private const string From = "alex-12@ukr.net";
		private readonly CoworkersTotalizatorContext _context;
		private readonly string[] _emailDomains;
		private readonly string _password;

		public LoginService(CoworkersTotalizatorContext context, string mailPass, params string[] emailDomains)
		{
			this._context = context;
			this._emailDomains = emailDomains;
			this._password = mailPass;
		}

		public void GetToken(string userName)
		{
			if (this._emailDomains.Any(userName.EndsWith) && IsValidEmail(userName))
			{
				var token = this.UpsertUser(userName);

				SendMail(userName, token.ToString());
			}
			else
			{
				throw new ArgumentException($"{userName} is in invalid domain");
			}
		}

		public bool Validate(Guid token, bool isAdminOnly = false)
		{
			var twentyMinutesAgo = DateTime.UtcNow - TimeSpan.FromMinutes(20);
			var existingToken = this._context.TokenHistory.FirstOrDefault(x => x.Id == token && x.CreatedAt >= twentyMinutesAgo);
			return existingToken != null && this.IsAccesible(existingToken, isAdminOnly);
		}

		public string GetRole(Guid token)
		{
			var existingToken = this._context.TokenHistory.FirstOrDefault(x => x.Id == token);
			var isAdmin = this._context.Users.First(x => x.Name == existingToken.UserId).IsAdmin;
			return isAdmin ? "Admin" : "User";
		}

		private bool IsAccesible(Token token, bool isAdminOnly)
		{
			return !isAdminOnly || this._context.Users.First(x => x.Name == token.UserId).IsAdmin;
		}

		private Guid UpsertUser(string userName)
		{
			var token = Guid.NewGuid();

			if (this._context.Users.Any(x => x.Name == userName))
			{
				var twentyMinutesAgo = DateTime.UtcNow - TimeSpan.FromMinutes(20);
				var existingToken = this._context.TokenHistory.FirstOrDefault(x => x.UserId == userName && x.CreatedAt >= twentyMinutesAgo);

				if (existingToken != null)
				{
					return existingToken.Id;
				}

				this._context.TokenHistory.Add(new Token
				{
					Id = token,
					CreatedAt = DateTime.UtcNow,
					UserId = userName
				});
			}
			else
			{
				this._context.Users.Add(new User
				{
					IsAdmin = false,
					Name = userName
				});

				this._context.TokenHistory.Add(new Token
				{
					Id = token,
					CreatedAt = DateTime.UtcNow,
					UserId = userName
				});
			}

			this._context.SaveChanges();

			return token;
		}

		private void SendMail(string userName, string token)
		{
			var message = new MimeMessage();
			message.From.Add(new MailboxAddress("Alex T", From));
			message.To.Add(new MailboxAddress(userName));
			message.Subject = "Totalizator Token";

			message.Body = new TextPart("plain")
			{
				Text = $"Here's yuor token: '{token}'. It's valid for 20 minutes"
			};

			using (var client = new SmtpClient())
			{
				client.ServerCertificateValidationCallback = (s, c, h, e) => true;

				client.Connect("smtp.ukr.net", 465, true);

				client.Authenticate(From, this._password);

				client.Send(message);
				client.Disconnect(true);
			}
		}

		private static bool IsValidEmail(string email)
		{
			try
			{
				var addr = new MailboxAddress(email);
				return addr.Address == email;
			}
			catch
			{
				return false;
			}
		}
	}
}
