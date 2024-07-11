using SmartWorkout.DTO;
using SmartWorkout.Entities;

namespace SmartWorkout.Repositories.Interfaces;

public interface IExerciseLogRepository
{
	ICollection<ExerciseLog> GetExerciseLogs();

	void SaveExerciseLog(ExerciseLogDTO exerciseLog);

	ICollection<ExerciseLog> GetAllExerciseLogsByWorkoutId(int workoutId);

	void DeleteExerciseLog(int exerciseLogId);

	ExerciseLogDTO GetExerciseLogById(int exerciseLogId);

	void UpdateExerciseLog(int? id, ExerciseLogDTO exerciseLog);
}	