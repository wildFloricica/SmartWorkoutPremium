using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using SmartWorkout.Components.Services.Interfaces;
using SmartWorkout.DTO;
using SmartWorkout.Repositories.Implementations;
using SmartWorkout.Repositories.Interfaces;

namespace SmartWorkout.Components.Layout
{
	public partial class NavMenu : ComponentBase
	{
		[Inject]
		public IAuthorizationService AuthorizationService { get; set; }
		[Inject]
		public NavigationManager NavigationManager { get; set; }
		[Inject] 
		public ProtectedSessionStorage SessionStorage { get; set; }
		[Inject]
		public IUserRepository UserRepository { get; set; }
		private UserDTO? User { get; set; }

		bool IsAdmin = false;

		bool IsLoggedIn { get; set; } = false;


		protected override void OnParametersSet()
		{
			
		}

		public async Task GoToWorkouts()
		{
			var user = await SessionStorage.GetAsync<UserDTO>("UserSession");
			if (user.Value == null)
			{
				return;
			}
			User = UserRepository.GetUserById(user.Value.Id);
			NavigationManager.NavigateTo($"/workouts/{User.Id}",true);
		}

	}
}
