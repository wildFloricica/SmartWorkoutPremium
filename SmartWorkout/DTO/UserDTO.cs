using System.ComponentModel.DataAnnotations;

namespace SmartWorkout.DTO;

public class UserDTO
{
	public int Id { get; set; }
	[Required]
	public string FirstName { get; set; }
	[Required]
	public string LastName { get; set; }
	[Required]
	public DateTime Birthday { get; set; }
	[Required]
	public string Gender { get; set; }

	[Required]
	public string Email { get; set; }


}