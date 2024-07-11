using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using SmartWorkout.Components.Services.Interfaces;
using SmartWorkout.DTO;
using SmartWorkout.Entities;
using SmartWorkout.Repositories.Implementations;
using SmartWorkout.Repositories.Interfaces;

namespace SmartWorkout.Components.Pages
{
	public partial  class EditExerciseLogsPage : ComponentBase
	{
		[Inject]
		public NavigationManager NavigationManager { get; set; }
		[Inject]
		public IUserRepository UserRepository { get; set; }
		[Inject]
		public IExerciseLogRepository ExerciseLogRepository { get; set; }
		[Inject]
		public IExerciseRepository ExerciseRepository { get; set; }
		[Inject]
		public ProtectedSessionStorage SessionStorage { get; set; }
		[Parameter]
		public int ExerciseLogId { get; set; }
		[Inject]
		public IAuthorizationService AuthorizationService { get; set; }
		private ICollection<ExerciseLog> ExerciseLogs { get; set; }
		public ExerciseLogDTO ExerciseLog { get; set; }
		public ICollection<Exercise> Exercises { get; set; }


		protected override  void OnInitialized()
		{
			
			Exercises = ExerciseRepository.GetExercises();
			ExerciseLog = ExerciseLogRepository.GetExerciseLogById(ExerciseLogId);

		}

		public void SaveCurrentExerciseLog()
		{
			ExerciseLogRepository.UpdateExerciseLog(ExerciseLogId,ExerciseLog);
			NavigationManager.NavigateTo($"/exercise-logs/add/{ExerciseLog.WorkoutId}");
		}
	}
}
