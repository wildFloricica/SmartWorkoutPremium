using SmartWorkout.DTO;
using SmartWorkout.Entities;

namespace SmartWorkout.Repositories.Interfaces;

public interface IWorkoutRepository
{
	ICollection<Workout> GetWorkouts();

	void SaveWorkout(WorkoutDTO workout);

	void DeleteWorkout(int id);

	WorkoutDTO GetWorkoutById(int id);
}