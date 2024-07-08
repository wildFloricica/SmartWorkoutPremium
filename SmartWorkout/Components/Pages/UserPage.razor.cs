using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using SmartWorkout.DTO;
using SmartWorkout.Entities;
using SmartWorkout.Repositories.Interfaces;

namespace SmartWorkout.Components.Pages;

public partial class UserPage : ComponentBase
{
	[Inject]
	public NavigationManager NavigationManager { get; set; }
	[Inject]
	public IUserRepository UserRepository { get; set; }

	private ICollection<User> Users;

	private User SelectedUser;

	private DeleteConfirmationDialog DeleteConfirmation;



	protected override void OnInitialized()
	{
		Users = UserRepository.GetUsers();
	}
	public void EditUser(EditCommandContext<User> context)
	{
		if (context != null && context.Item != null)
		{
			NavigationManager.NavigateTo($"/users/edit/{context.Item.Id}");
		}
	}

	public void AddWorkout(EditCommandContext<User> context)
	{
		if (context != null && context.Item != null)
		{
			NavigationManager.NavigateTo($"/workouts/add/{context.Item.Id}");
		}
	}

	public void AddUser()
	{
		NavigationManager.NavigateTo("/users/add");
	}

	private void OnDeleteButtonClicked(CommandContext<User?> context)
	{
		SelectedUser = context.Item;
		if (DeleteConfirmation is null || SelectedUser is null)
		{
			return;
		}

		DeleteConfirmation.Show();
	}

	private async Task HandleDeleteConfirmed(bool confirmed)
	{
		if (SelectedUser != null)
		{
			UserRepository.DeleteUser(SelectedUser.Id);
			OnInitialized();
		}
	}
}