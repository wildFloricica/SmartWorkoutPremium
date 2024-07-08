using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using SmartWorkout.Entities;
using SmartWorkout.Repositories.Implementations;
using SmartWorkout.Repositories.Interfaces;

namespace SmartWorkout.Components.Pages;

public partial class WorkoutsPage : ComponentBase
{
	[Inject] public NavigationManager NavigationManager { get; set; }

	[Inject] public IWorkoutRepository WorkoutRepository { get; set; }

	private ICollection<Workout> Workouts;

	protected override void OnInitialized()
	{
		Workouts = WorkoutRepository.GetWorkouts();
	}

	private Workout SelectedWorkout { get; set; }

	private DeleteConfirmationDialog DeleteConfirmation { get; set; }

	private void OnDeleteButtonClicked(CommandContext<Workout?> context)
	{
		SelectedWorkout = context.Item;
		if (DeleteConfirmation is null || SelectedWorkout is null)
		{
			return;
		}

		DeleteConfirmation.Show();
	}

	private async Task HandleDeleteConfirmed(bool confirmed)
	{
		if (SelectedWorkout != null)
		{
			WorkoutRepository.DeleteWorkout(SelectedWorkout.Id);
			OnInitialized();
		}
	}

	public void EditWorkout(EditCommandContext<Workout> context)
	{
		if (context != null && context.Item != null)
		{
			NavigationManager.NavigateTo($"/exercise-logs/add/{context.Item.Id}");
		}
	}
}