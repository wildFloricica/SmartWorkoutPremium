using Microsoft.AspNetCore.Components;
using SmartWorkout.Components.Exceptions;
using SmartWorkout.Components.Services.Interfaces;
using SmartWorkout.DTO;
using SmartWorkout.Entities;
using SmartWorkout.Repositories.Interfaces;

namespace SmartWorkout.Components.Services.Implementations
{
    public class AuthorizationService : IAuthorizationService
    {
	    private readonly IUserRepository _userRepository;

	    public AuthorizationService(IUserRepository userRepository)
	    {
		    _userRepository = userRepository;
	    }

        public static UserDTO CurrentUser { get;set;}
  

        public void Login(LoginDTO loginDto)
        {
	        if (_userRepository.existsByEmail(loginDto.Email))
	        {
		        UserDTO userToLogin = _userRepository.GetUserByEmail(loginDto.Email);

		        if (loginDto.Password == userToLogin.Birthday.ToString("MMyyyy"))
		        {
			        CurrentUser = userToLogin;
				}
		        else
		        {
					throw new InvalidCredentialsException("Wrong username or password");
				}
			}
	        else
	        {
		        throw new InvalidCredentialsException("Wrong username or password");
			}
	        
	        
        }

        public UserDTO GetCurrentUser()
        {
	        return CurrentUser;
        }

        public bool IsUserPresent()
        {
	        if (CurrentUser == null)
	        {
		        return false;
	        }

	        return true;
        }
    }
}
