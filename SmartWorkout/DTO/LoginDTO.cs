using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartWorkout.DTO
{
	public class LoginDTO
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[PasswordPropertyText]
		public string Password { get; set; }

	}
}
