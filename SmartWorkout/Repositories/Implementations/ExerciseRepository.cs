using SmartWorkout.Context;
using SmartWorkout.DTO;
using SmartWorkout.Entities;
using SmartWorkout.Repositories.Interfaces;

namespace SmartWorkout.Repositories.Implementations;

public class ExerciseRepository : IExerciseRepository
{
	private readonly SmartWorkoutContext _context;
	public ExerciseRepository(SmartWorkoutContext context)
	{
		_context = context;
	}
	public ICollection<Exercise> GetExercises()
	{
		var exercises = _context.Exercises.ToList();
		return exercises;
	}

	public void SaveExercise(ExerciseDTO exercise)
	{
		Exercise exerciseToAdd = new Exercise()
		{
			Type = exercise.Type,
			Description = exercise.Description
		};

		_context.Exercises.Add(exerciseToAdd);
		_context.SaveChanges();
	}

	public void UpdateExercise(int? id, ExerciseDTO exercise)
	{


		Exercise exerciseToUpdate = _context.Exercises.FirstOrDefault(x => x.Id == id);

		if (exerciseToUpdate != null)
		{
			exerciseToUpdate.Type = exercise.Type;
			exerciseToUpdate.Description = exercise.Description;

			_context.Exercises.Update(exerciseToUpdate);
			_context.SaveChanges();
		}
		else
		{
			throw new Exception($"Exercise with id {id} not found!");
		}
	}

	public void DeleteExercise(int? id)
	{
		Exercise existingExercise = _context.Exercises.FirstOrDefault(x => x.Id == id);
		if (existingExercise != null)
		{
			_context.Exercises.Remove(existingExercise);
			_context.SaveChanges();
		}
		else
		{
			throw new Exception("Exercise not found");
		}
	}

	public ExerciseDTO GetExerciseById(int? id)
	{
		Exercise existingExercise = _context.Exercises.FirstOrDefault(x => x.Id == id);


		if (existingExercise == null)
		{
			throw new Exception("Exercise not found!");
		}


		ExerciseDTO existingExerciseDto = new ExerciseDTO()
		{
			Description = existingExercise.Description,
			Type = existingExercise.Type
		};
		return existingExerciseDto;
	}
}