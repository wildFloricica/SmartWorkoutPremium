using SmartWorkout.DTO;
using SmartWorkout.Entities;

namespace SmartWorkout.Repositories.Interfaces
{
	public interface IExerciseRepository
	{
		ICollection<Exercise> GetExercises();

		void SaveExercise(ExerciseDTO exercise);

		void UpdateExercise(int? id , ExerciseDTO exercise);

		void DeleteExercise(int? id);

		ExerciseDTO GetExerciseById(int? id);
	}
}
