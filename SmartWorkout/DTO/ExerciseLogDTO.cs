using System.ComponentModel.DataAnnotations;

namespace SmartWorkout.DTO;

public class ExerciseLogDTO
{
	[Required]
	[Range(1,100)]
	public int Reps { get; set; }
	[Required]
	[Range(1, 100)]
	public int Duration { get; set; }
	[Required]
	public int ExerciseId { get; set; }
	[Required]
	public int WorkoutId { get; set; }

}