using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SmartWorkout.DTO;
using SmartWorkout.Entities;
using SmartWorkout.Repositories.Implementations;
using SmartWorkout.Repositories.Interfaces;
namespace SmartWorkout.Components.Pages
{
	public partial class AddOrEditUserPage : ComponentBase
	{
		public EditContext EditContext { get; set; }
		public ValidationMessageStore MessageStore { get; set; }
		[Inject]
		public IUserRepository UserRepository { get; set; }

		[Inject]
		public NavigationManager NavigationManager { get; set; }

		[SupplyParameterFromForm]
		public UserDTO User { get; set; } = new UserDTO();

		[Parameter]
		public int? UserId { get; set; }
		public void SaveCurrentUser(EditContext editContext)
		{
			if (UserId == null)
			{
				UserRepository.SaveUser(User);
			}
			else
			{
				UserRepository.UpdateUser(UserId, User);
			}

			// Navigate to the users page
			NavigationManager.NavigateTo("/users");
		}

		private void HandleFieldChanged(object? sender, ValidationRequestedEventArgs args)
		{
			MessageStore?.Clear();

			var exists = UserRepository.existsByEmail(User.Email);
			if (exists && UserRepository.GetUserByEmail(User.Email).Id != UserId && User.Email.Length > 0)
			{
				MessageStore.Add(() => User.Email, "Email is already used!");
				EditContext.NotifyValidationStateChanged();

			}
		}

		protected override void OnParametersSet()
		{
			if (UserId != null)
			{
				User = UserRepository.GetUserById(UserId);
			}

			EditContext = new EditContext(User);
			MessageStore = new ValidationMessageStore(EditContext);

			EditContext.OnValidationRequested += HandleFieldChanged;
		}


	}
}
