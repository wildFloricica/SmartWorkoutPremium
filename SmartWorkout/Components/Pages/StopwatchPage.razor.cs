using Microsoft.AspNetCore.Components;
using System.Timers;
using SmartWorkout.DTO;
using SmartWorkout.Repositories.Interfaces;

namespace SmartWorkout.Components.Pages
{
	public partial class StopwatchPage : ComponentBase, IDisposable
	{
		[Inject] public NavigationManager NavigationManager { get; set; }
		[Inject] public IExerciseLogRepository ExerciseLogRepository { get; set; }
		[Inject] public IExerciseRepository ExerciseRepository { get; set; }

		public int PercentageRemaining { get; set; } = 100;
		public ExerciseLogDTO ExerciseLog { get; set; }
		public ExerciseDTO Exercise { get; set; }
		public string ErrorMessage { get; set; }
		public System.Timers.Timer Timer { get; set; }
		public TimeSpan RemainingTime { get; set; }
		public int RemainingReps { get; set; }
		public int InitialSeconds { get; set; }
		public bool IsRunning { get; set; }

		public bool CountdownFinished { get; set; }
		[Parameter]
		public int ExerciseLogId { get; set; }

		public string FormattedTime => $"{RemainingTime.Minutes:D2}:{RemainingTime.Seconds:D2}";

		protected override void OnParametersSet()
		{
			Timer = new System.Timers.Timer(100); // Updatează la fiecare 100 ms
			Timer.Elapsed += UpdateTime;
			ExerciseLog = ExerciseLogRepository.GetExerciseLogById(ExerciseLogId);
			Exercise = ExerciseRepository.GetExerciseById(ExerciseLog.ExerciseId);
			InitialSeconds = ExerciseLog.Duration;
			RemainingReps = ExerciseLog.Reps;
			RemainingTime = TimeSpan.FromSeconds(InitialSeconds);
		}

		private void StartCountdown()
		{
			if (RemainingReps > 0)
			{
				if (!IsRunning)
				{
					Timer.Start();
					IsRunning = true;
					CountdownFinished = false;
				}
			}
			else
			{
				ErrorMessage = "Exercise is Done!";
			}
		}

		private void StopCountdown()
		{
			Timer.Stop();
			IsRunning = false;
		}

		private void ResetCountdown()
		{
			StopCountdown();
			RemainingTime = TimeSpan.FromSeconds(InitialSeconds);
			PercentageRemaining = 100;
			CountdownFinished = false;
		}

		private void UpdateTime(object sender, ElapsedEventArgs e)
		{
			RemainingTime -= TimeSpan.FromMilliseconds(100);
			PercentageRemaining = (int)((100 * (RemainingTime.TotalSeconds) / InitialSeconds));
			if (RemainingTime <= TimeSpan.Zero)
			{
				RemainingTime = TimeSpan.Zero;
				Timer.Stop();
				IsRunning = false;
				CountdownFinished = true;
				RemainingReps -= 1;
				ResetCountdown();
			}

			InvokeAsync(StateHasChanged);
		}

		public void Dispose()
		{
			Timer?.Dispose();
		}

		public void GoBack()
		{
			NavigationManager.NavigateTo($"exercise-logs/add/{ExerciseLog.WorkoutId}");
		}
	}
}
