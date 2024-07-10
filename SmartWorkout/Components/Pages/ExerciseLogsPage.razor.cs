using Microsoft.AspNetCore.Components;
using SmartWorkout.Components.Services.Interfaces;
using SmartWorkout.DTO;
using SmartWorkout.Entities;
using SmartWorkout.Repositories.Interfaces;

namespace SmartWorkout.Components.Pages
{
	public partial  class ExerciseLogsPage : ComponentBase
	{
		[Inject]
		public IExerciseLogRepository ExerciseLogRepository { get; set; }
		[Inject]
		public IAuthorizationService AuthorizationService { get; set; }
		private ICollection<ExerciseLog> ExerciseLogs { get; set; }

		UserDTO User { get; set; }

		protected override void OnInitialized()
		{
			User = null;

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
