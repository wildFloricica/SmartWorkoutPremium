using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using SmartWorkout.DTO;
using SmartWorkout.Entities;
using SmartWorkout.Repositories.Implementations;
using SmartWorkout.Repositories.Interfaces;

namespace SmartWorkout.Components.Pages
{
	public partial class HomePage : ComponentBase
	{
		[Inject] public ProtectedSessionStorage SessionStorage { get; set; }
		[Inject] public IUserRepository UserRepository { get; set; }
		[Inject] public NavigationManager NavigationManager { get; set; }
		
		public UserDTO User { get; set; }

		protected override async Task OnInitializedAsync()
		{
			var user = await SessionStorage.GetAsync<UserDTO>("UserSession");
			if (user.Success)
			{
				User = UserRepository.GetUserById(user.Value.Id);
			}

		}
		public void GoToWorkouts()
		{
			NavigationManager.NavigateTo($"/workouts/{User.Id}");	
		}
	}
}
