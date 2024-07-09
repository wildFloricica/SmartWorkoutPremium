using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using SmartWorkout.DTO;
using SmartWorkout.Entities;
using SmartWorkout.Repositories.Implementations;
using SmartWorkout.Repositories.Interfaces;

namespace SmartWorkout.Components.Pages;

public partial class WorkoutsPage : ComponentBase
{
	[Inject] public NavigationManager NavigationManager { get; set; }

	[Inject] public IWorkoutRepository WorkoutRepository { get; set; }
	[Inject] public IUserRepository UserRepository { get; set; }

	[Parameter] public int? UserId { get; set; }  


	private ICollection<Workout> Workouts;
	
	public UserDTO User { get; set; }

	
	private Workout SelectedWorkout { get; set; }

	private DeleteConfirmationDialog DeleteConfirmation { get; set; }
	private bool UserIdIsPresent = false;

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
			OnParametersSet();
		}
	}

	public void EditWorkout(EditCommandContext<Workout> context)
	{
		if (context != null && context.Item != null)
		{
			NavigationManager.NavigateTo($"/exercise-logs/add/{context.Item.Id}");
		}
	}

	public void AddWorkout()
	{
		NavigationManager.NavigateTo($"/workouts/add/{UserId}");
	}

	protected override void OnParametersSet()
	{
		
		if (UserId != null)
		{
			Workouts = WorkoutRepository.GetAllWorkoutsByUserId(UserId.Value);
			UserIdIsPresent = true;
			User = UserRepository.GetUserById(UserId);
		}
		else
		{
			Workouts = WorkoutRepository.GetWorkouts();
		}
	}

}