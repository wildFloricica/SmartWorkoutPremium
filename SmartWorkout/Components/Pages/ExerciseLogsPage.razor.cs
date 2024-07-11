using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using SmartWorkout.Components.Services.Interfaces;
using SmartWorkout.DTO;
using SmartWorkout.Entities;
using SmartWorkout.Repositories.Implementations;
using SmartWorkout.Repositories.Interfaces;

namespace SmartWorkout.Components.Pages
{
	public partial  class ExerciseLogsPage : ComponentBase
	{
		[Inject]
		public IUserRepository UserRepository { get; set; }
		[Inject]
		public IExerciseLogRepository ExerciseLogRepository { get; set; }
		[Inject]
		public ProtectedSessionStorage SessionStorage { get; set; }
		[Inject]
		public IAuthorizationService AuthorizationService { get; set; }
		private ICollection<ExerciseLog> ExerciseLogs { get; set; }

		UserDTO? User { get; set; }

		protected override async Task OnInitializedAsync()
		{
			var user = await SessionStorage.GetAsync<UserDTO>("UserSession");
			User = UserRepository.GetUserById(user.Value.Id);
		

			if (User.IsAdmin)
			{
				ExerciseLogs = ExerciseLogRepository.GetExerciseLogs();
			}
			else
			{
				ExerciseLogs = ExerciseLogRepository.GetExerciseLogs().Where(x => x.Workout.UserId == User.Id).ToList();
			}
			
		}
	}
}
