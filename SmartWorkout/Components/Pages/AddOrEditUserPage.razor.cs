using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SmartWorkout.DTO;
using SmartWorkout.Entities;
using SmartWorkout.Repositories.Implementations;
using SmartWorkout.Repositories.Interfaces;
namespace SmartWorkout.Components.Pages
{
	public partial class AddOrEditUserPage : ComponentBase
	{
		[Inject]
		public IUserRepository UserRepository { get; set; }

		[Inject]
		public NavigationManager NavigationManager { get; set; }

		[SupplyParameterFromForm]
		public UserDTO User { get; set; } = new UserDTO();

		[Parameter]
		public int? UserId { get; set; }
		public async Task SaveCurrentUser()
		{
			if (UserId == null)
			{
				UserRepository.SaveUser(User);
				await InvokeAsync(() => NavigationManager.NavigateTo("/users"));
			}
			else
			{
				UserRepository.UpdateUser(UserId,User);
				await InvokeAsync(() => NavigationManager.NavigateTo("/users"));
			}

		}

		protected override void OnParametersSet()
		{
			if (UserId != null)
			{
				User = UserRepository.GetUserById(UserId);
			}
		}

		
	}
}
