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
	[EmailAddress]
	[RegularExpression("^[A-Za-z0-9.%+-]+@[A-Za-z0-9.-]+\\.[A-Z|a-z]{2,}$", ErrorMessage = "Invalid email adress")]
	public string Email { get; set; }

	public bool IsAdmin { get; set; }


}