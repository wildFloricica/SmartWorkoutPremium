﻿using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using SmartWorkout.Components.Services.Interfaces;
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

	[Inject] public IAuthorizationService AuthorizationService { get; set; }

	[Inject] public ProtectedSessionStorage SessionStorage { get; set; }
	[Parameter] public int? UserId { get; set; }


	private ICollection<Workout> Workouts;

	public UserDTO User { get; set; }

	public UserDTO CurrentUser { get; set; }
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
			OnParametersSetAsync();
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

	

	protected override async Task OnParametersSetAsync()
	{

		if (UserId != null)
		{
			Workouts = WorkoutRepository.GetAllWorkoutsByUserId(UserId.Value);
			UserIdIsPresent = true;
			var user =  await SessionStorage.GetAsync<UserDTO>("UserSession");
			User = UserRepository.GetUserById(user.Value.Id);

		}
		else
		{
			var user = await SessionStorage.GetAsync<UserDTO>("UserSession");
			User = UserRepository.GetUserById(user.Value.Id);
			if (User.IsAdmin)
			{
				Workouts = WorkoutRepository.GetWorkouts();
			}
			else
			{
				NavigationManager.NavigateTo($"/workouts/{User.Id}");
			}
		}

	}
}
