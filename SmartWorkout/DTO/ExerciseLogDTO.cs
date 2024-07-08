using System.ComponentModel.DataAnnotations;

namespace SmartWorkout.DTO;

public class ExerciseLogDTO
{
	[Required]
	public int Reps { get; set; }
	[Required]
	public int Duration { get; set; }
	[Required]
	public int ExerciseId { get; set; }
	[Required]
	public int WorkoutId { get; set; }

}