using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using SmartWorkout.Components.Services.Interfaces;
using SmartWorkout.DTO;
using SmartWorkout.Entities;
using SmartWorkout.Repositories.Implementations;
using SmartWorkout.Repositories.Interfaces;

namespace SmartWorkout.Components.Pages;

public partial class AddOrEditExerciseLogPage:ComponentBase
{
	[Inject]
	public IExerciseLogRepository ExerciseLogRepository { get; set; }

	[Inject]
	public IWorkoutRepository WorkoutRepository { get; set; }

	[Inject]
	public IExerciseRepository ExerciseRepository { get; set; }

	[Inject]
	public NavigationManager NavigationManager { get; set; }

	[Inject]
	public IAuthorizationService AuthorizationService { get; set; }
	[SupplyParameterFromForm]
	public ExerciseLogDTO ExerciseLog { get; set; } = new ExerciseLogDTO();

	[Parameter]
	public int WorkoutId { get; set; }

	public WorkoutDTO Workout { get; set; }

	private DeleteConfirmationDialog DeleteConfirmation;

	public ExerciseLog SelectedExerciseLog { get; set; }
	public ICollection<ExerciseLog> ExerciseLogs { get; set; }
	public ICollection<Workout> Workouts { get; set; }
	public ICollection<Exercise> Exercises { get; set; }

    private bool _accordionItem1Visible = true;
    private bool _accordionItem2Visible = false;

	protected override void OnInitialized()
	{
		ExerciseLog = new ExerciseLogDTO()
		{
			Duration = 0,
			ExerciseId = 1,
			WorkoutId = WorkoutId
		};
		Workouts = WorkoutRepository.GetWorkouts();
		Exercises = ExerciseRepository.GetExercises();
		ExerciseLogs = ExerciseLogRepository.GetAllExerciseLogsByWorkoutId(WorkoutId);
	}
	public void SaveCurrentExerciseLog()
	{
		ExerciseLogRepository.SaveExerciseLog(ExerciseLog);
		OnInitialized();
	}

	protected override void OnParametersSet()
	{
		Workout = WorkoutRepository.GetWorkoutById(WorkoutId);
		ExerciseLog.WorkoutId = WorkoutId;
		
	}

	private void OnDeleteButtonClicked(CommandContext<ExerciseLog?> context)
	{
		SelectedExerciseLog = context.Item;
		if (DeleteConfirmation is null || SelectedExerciseLog is null)
		{
			return;
		}

		DeleteConfirmation.Show();
	}

	private async Task HandleDeleteConfirmed(bool confirmed)
	{
		if (SelectedExerciseLog != null)
		{
			ExerciseLogRepository.DeleteExerciseLog(SelectedExerciseLog.Id);
			OnInitialized();
		}
	}

	public void EditExerciseLog(EditCommandContext<ExerciseLog> context)
	{
		if (context != null && context.Item != null)
		{
			NavigationManager.NavigateTo($"/exercise-log/edit/{context.Item.Id}");
		}
	}

	public void StartWorkout(EditCommandContext<ExerciseLog> context)
	{
		if (context != null && context.Item != null)
		{
			NavigationManager.NavigateTo($"/stopwatch/{context.Item.Id}");
		}
	}
}