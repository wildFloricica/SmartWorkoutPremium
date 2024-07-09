using System.ComponentModel.DataAnnotations;

namespace SmartWorkout.DTO;

public class WorkoutDTO
{
	[Required]
	public string Name { get; set; }
	[Required]
	public DateTime Date { get; set; }
	public int UserId { get; set; }
}