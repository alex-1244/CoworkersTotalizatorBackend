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
		private string _password;

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

		public bool Validate(Guid token)
		{
			var twentyMinutesAgo = DateTime.UtcNow - TimeSpan.FromMinutes(20);
			if (this._context.TokenHistory.Any(x => x.Id == token && x.CreatedAt >= twentyMinutesAgo))
			{
				return true;
			}

			return false;
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
