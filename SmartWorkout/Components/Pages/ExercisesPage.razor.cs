using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using SmartWorkout.Entities;
using SmartWorkout.Repositories.Interfaces;

namespace SmartWorkout.Components.Pages
{
	public partial class ExercisesPage : ComponentBase
	{
		[Inject]
		public IExerciseRepository ExerciseRepository { get; set; }
		[Inject]
		public NavigationManager NavigationManager { get; set; }

		private ICollection<Exercise> Exercises;

		private Exercise SelectedExercise;

		private DeleteConfirmationDialog DeleteConfirmation;

		protected override void OnInitialized()
		{
			Exercises = ExerciseRepository.GetExercises();
		}

		public void EditExercise(EditCommandContext<Exercise> context)
		{
			if (context != null && context.Item != null)
			{
				NavigationManager.NavigateTo($"/exercises/edit/{context.Item.Id}");
			}
		}

		public void AddExercise()
		{
			NavigationManager.NavigateTo("/exercises/add");
		}

		private void OnDeleteButtonClicked(CommandContext<Exercise> context)
		{
			SelectedExercise = context.Item;
			if (DeleteConfirmation is null || SelectedExercise is null)
			{
				return;
			}

			DeleteConfirmation.Show();
		}

		private async Task HandleDeleteConfirmed(bool confirmed)
		{
			if (SelectedExercise != null)
			{
				ExerciseRepository.DeleteExercise(SelectedExercise.Id);
				OnInitialized();
			}
		}
	}
}
