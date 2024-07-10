/*
using SmartWorkout.Entities;

namespace SmartWorkout.Components.Services.Implementations
{
	public class UserAccountService
	{
		public class UserAccountService
		{
			private List<User> _users;

			public UserAccountService()
			{
				_users = new List<UserAccount>
				{
					new UserAccount{ UserName = "admin", Password = "admin", Role = "Administrator" },
					new UserAccount{ UserName = "user", Password = "user", Role = "User" },
				};
			}

			//UserAccount? means that the method GetByUserName can return a valid UserAccount object or null. 
			public UserAccount? GetByUserName(string userName)
			{
				return _users.FirstOrDefault(x => x.UserName == userName);
			}
		}
	}
}
*/
