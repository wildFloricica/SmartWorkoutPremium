using Microsoft.AspNetCore.Components;
using SmartWorkout.Components.Exceptions;
using SmartWorkout.Components.Services.Implementations;
using SmartWorkout.Components.Services.Interfaces;
using SmartWorkout.DTO;
using SmartWorkout.Entities;
using SmartWorkout.Repositories.Implementations;
using SmartWorkout.Repositories.Interfaces;

namespace SmartWorkout.Components.Pages
{
	public partial class LoginPage : ComponentBase
	{
		

		[Inject]
		public IAuthorizationService AuthorizationService { get; set; }

		[Inject]
		public NavigationManager NavigationManager { get; set; }

		[SupplyParameterFromForm]
		public LoginDTO LoginDTO { get; set; } = new LoginDTO();

		public string ErrorMessage { get; set; }
		protected override void OnInitialized()
		{
			ErrorMessage = null;
		}

		public void Login()
		{
			try
			{
				AuthorizationService.Login(LoginDTO);
				NavigationManager.NavigateTo("/home");
			}
			catch (InvalidCredentialsException e)
			{
				Console.WriteLine(e);
				ErrorMessage = e.Message;
			}

			StateHasChanged();
			
		}


	}
}
