using System.ComponentModel.DataAnnotations;

namespace SmartWorkout.DTO;

public class UserDTO
{
	[Required]
	public string FirstName { get; set; }
	[Required]
	public string LastName { get; set; }
	[Required]
	public DateTime Birthday { get; set; }
	[Required]
	public string Gender { get; set; }
}