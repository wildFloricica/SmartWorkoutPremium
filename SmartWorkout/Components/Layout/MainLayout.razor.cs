using Microsoft.AspNetCore.Components;
using SmartWorkout.Components.Services.Interfaces;
using SmartWorkout.DTO;

namespace SmartWorkout.Components.Layout
{
	public partial class MainLayout : LayoutComponentBase
	{
		[Inject]
		public IAuthorizationService AuthorizationService { get; set; }
		[Inject]
		public NavigationManager NavigationManager { get; set; }
		private UserDTO? User { get; set; }

		bool IsLoggedIn { get; set; } = false;

		

		public void LogOut()
		{
			
		}
	}
}