using Blazorise;
using Microsoft.AspNetCore.Components;
using SmartWorkout.DTO;
using SmartWorkout.Entities;
using SmartWorkout.Repositories.Implementations;
using SmartWorkout.Repositories.Interfaces;

namespace SmartWorkout.Components.Pages;

public partial class AddOrEditExercisePage : ComponentBase
{
	[Inject]
	NavigationManager NavigationManager { get; set; }

	[Inject]
	public IExerciseRepository ExerciseRepository { get; set; }

	[SupplyParameterFromForm]
	public ExerciseDTO Exercise { get; set; } = new ExerciseDTO();

	[Parameter]
	public int ?ExerciseId { get; set; }

	public async Task SaveCurrentExercise()
	{
		if (ExerciseId == null)
		{
			ExerciseRepository.SaveExercise(Exercise);
			await InvokeAsync(() => NavigationManager.NavigateTo("/exercises"));
		}
		else
		{
			ExerciseRepository.UpdateExercise(ExerciseId,Exercise);
			await InvokeAsync(() => NavigationManager.NavigateTo("/exercises"));
		}

	}

	protected override void OnParametersSet()
	{
		if (ExerciseId != null)
		{
			Exercise = ExerciseRepository.GetExerciseById(ExerciseId);
		}
	}

}