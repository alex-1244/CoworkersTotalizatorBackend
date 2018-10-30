using CoworkersTotalizator.Models.Users;

namespace CoworkersTotalizator.Services
{
	public interface ICurrentUserAccessor
	{
		User GetCurrentUser();
	}
}
