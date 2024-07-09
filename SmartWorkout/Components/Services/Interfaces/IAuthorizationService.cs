using SmartWorkout.DTO;

namespace SmartWorkout.Components.Services.Interfaces
{
	public interface IAuthorizationService
	{
		public void Login(LoginDTO loginDto);

		public UserDTO GetCurrentUser();

		bool IsUserPresent();
	}
}
