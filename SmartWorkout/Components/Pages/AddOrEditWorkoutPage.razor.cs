using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Components;
using SmartWorkout.DTO;
using SmartWorkout.Entities;
using SmartWorkout.Repositories.Interfaces;

namespace SmartWorkout.Components.Pages
{
	public partial class AddOrEditWorkoutPage : ComponentBase
	{
		[SupplyParameterFromForm]
		public WorkoutDTO Workout { get; set; } = new WorkoutDTO();
		[Inject]
		public IWorkoutRepository WorkoutRepository { get; set; }

		[Inject]
		public IUserRepository UserRepository { get; set; }

		[Inject]
		public NavigationManager NavigationManager { get; set; }
		[Parameter]
		public int UserId { get; set; }

		public ICollection<User> Users { get; set; }

		private UserDTO User { get; set; } 
		public void SaveCurrentWorkout()
		{
			if (UserId != null)
			{
				WorkoutRepository.SaveWorkout(Workout);
				InvokeAsync(() => NavigationManager.NavigateTo($"/users"));
			}
		}

		protected override void OnInitialized()
		{
			Users = UserRepository.GetUsers();
		}

		protected override void OnParametersSet()
		{
				User = UserRepository.GetUserById(UserId);
				Workout.UserId = UserId;
		}
	}
}