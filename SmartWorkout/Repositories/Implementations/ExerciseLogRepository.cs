using Microsoft.EntityFrameworkCore;
using SmartWorkout.Context;
using SmartWorkout.DTO;
using SmartWorkout.Entities;
using SmartWorkout.Repositories.Interfaces;

namespace SmartWorkout.Repositories.Implementations;

public class ExerciseLogRepository : IExerciseLogRepository
{
	public ExerciseLogRepository(SmartWorkoutContext context)
	{
		_context = context;
	}

	private readonly SmartWorkoutContext _context;
	public ICollection<ExerciseLog> GetExerciseLogs()
	{
		var exerciseLogs = _context.ExerciseLogs.Include(x=> x.Exercise).Include(x=> x.Workout.User).ToList();
		return exerciseLogs;
	}

	public void SaveExerciseLog(ExerciseLogDTO exerciseLog)
	{
		ExerciseLog exerciseLogToAdd = new ExerciseLog()
		{
			Duration = exerciseLog.Duration,
			Reps = exerciseLog.Reps,
			ExerciseId = exerciseLog.ExerciseId,
			WorkoutId = exerciseLog.WorkoutId
		};
		_context.ExerciseLogs.Add(exerciseLogToAdd);
		_context.SaveChanges();
	}

	public ICollection<ExerciseLog> GetAllExerciseLogsByWorkoutId(int workoutId)
	{
		var exerciseLogs = _context.ExerciseLogs.Where(x => x.WorkoutId == workoutId).ToList();
		return exerciseLogs;
	}

	public void DeleteExerciseLog(int exerciseLogId)
	{
		ExerciseLog existingExerciseLog = _context.ExerciseLogs.FirstOrDefault(x => x.Id == exerciseLogId);
		if (existingExerciseLog != null)
		{
			_context.Remove(existingExerciseLog);
			_context.SaveChanges();
		}
		else
		{
			throw new Exception("Exercise Log not found");
		}
	
	}
}