using Microsoft.EntityFrameworkCore;
using SmartWorkout.Context;
using SmartWorkout.DTO;
using SmartWorkout.Entities;
using SmartWorkout.Repositories.Interfaces;

namespace SmartWorkout.Repositories.Implementations;

public class WorkoutRepository : IWorkoutRepository
{
	private readonly SmartWorkoutContext _context;

	public WorkoutRepository(SmartWorkoutContext context)
	{
		_context = context;
	}

	public ICollection<Workout> GetWorkouts()
	{
		var workouts = _context.Workouts.Include(x => x.User).ToList();
		return workouts;
	}

	public void SaveWorkout(WorkoutDTO workout)
	{
		Workout workoutToAdd = new Workout()
		{
			Name = workout.Name,
			Date = workout.Date,
			UserId = workout.UserId
		};

	  _context.Workouts.Add(workoutToAdd);
	  _context.SaveChanges();
	}

	public void DeleteWorkout(int id)
	{
		Workout existingWorkout = _context.Workouts.FirstOrDefault(x => x.Id == id);
		if (existingWorkout != null)
		{
			_context.Workouts.Remove(existingWorkout);
			_context.SaveChanges();
		}
		else
		{
			throw new Exception("Workout not found!");
		}
	}

	public WorkoutDTO GetWorkoutById(int id)
	{
		Workout existingWorkout = _context.Workouts.FirstOrDefault(w => w.Id == id);

		if (existingWorkout != null)
		{
			WorkoutDTO existingWorkoutDto = new WorkoutDTO()
			{
				Date = existingWorkout.Date,
				Name = existingWorkout.Name,
				UserId = existingWorkout.UserId
			};

			return existingWorkoutDto;
		}

		throw new Exception("Workout not found!");
	}

	public ICollection<Workout> GetAllWorkoutsByUserId(int userId)
	{
		ICollection<Workout> existingWorkouts = _context.Workouts.Where(x => x.UserId == userId).ToList();

		return existingWorkouts;
	}
}