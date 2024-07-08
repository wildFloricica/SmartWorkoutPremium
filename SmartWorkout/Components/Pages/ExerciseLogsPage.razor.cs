using Microsoft.AspNetCore.Components;
using SmartWorkout.Entities;
using SmartWorkout.Repositories.Interfaces;

namespace SmartWorkout.Components.Pages
{
	public partial  class ExerciseLogsPage : ComponentBase
	{
		[Inject]
		public IExerciseLogRepository ExerciseLogRepository { get; set; }
		private ICollection<ExerciseLog> ExerciseLogs { get; set; }

		protected override void OnInitialized()
		{
			ExerciseLogs = ExerciseLogRepository.GetExerciseLogs();
		}
	}
}
