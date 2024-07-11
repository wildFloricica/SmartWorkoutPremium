using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using SmartWorkout.Components.Services.Interfaces;
using SmartWorkout.DTO;
using SmartWorkout.Entities;
using SmartWorkout.Repositories.Implementations;
using SmartWorkout.Repositories.Interfaces;

namespace SmartWorkout.Components.Layout
{
	public partial class MainLayout : LayoutComponentBase
	{
		[Inject]
		public IAuthorizationService AuthorizationService { get; set; }
		[Inject]
		public NavigationManager NavigationManager { get; set; }
		[Inject]
		public IUserRepository UserRepository { get; set; }
		[Inject]
		public ProtectedSessionStorage SessionStorage { get; set; }
		private UserDTO? User { get; set; }

		bool IsLoggedIn { get; set; } = false;

		

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			var user = await SessionStorage.GetAsync<UserDTO>("UserSession");
			if (user.Success)
			{
				User = UserRepository.GetUserById(user.Value.Id);
			}

			await base.OnAfterRenderAsync(firstRender);
		}


		public void LogOut()
		{
			AuthorizationService.Logout();
			NavigationManager.NavigateTo("/login");
		}
	}
}