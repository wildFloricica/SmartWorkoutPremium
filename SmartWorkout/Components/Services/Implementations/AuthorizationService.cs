using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using SmartWorkout.Authentication;
using SmartWorkout.Components.Exceptions;
using SmartWorkout.Components.Services.Interfaces;
using SmartWorkout.DTO;
using SmartWorkout.Entities;
using SmartWorkout.Repositories.Interfaces;
using System.Security.Claims;

namespace SmartWorkout.Components.Services.Implementations
{
    public class AuthorizationService : IAuthorizationService
    {
	    private readonly IUserRepository _userRepository;

		private readonly CustomAuthenticationStateProvider _customAuthenticationStateProvider;

	    public AuthorizationService(IUserRepository userRepository, AuthenticationStateProvider authenticationStateProvider)
	    {
		    _userRepository = userRepository;
		    _customAuthenticationStateProvider = (CustomAuthenticationStateProvider)authenticationStateProvider;
	    }
  

        public void Login(LoginDTO loginDto)
        {
	        if (_userRepository.existsByEmail(loginDto.Email))
	        {
		        UserDTO userToLogin = _userRepository.GetUserByEmail(loginDto.Email);

		        if (loginDto.Password == userToLogin.Birthday.ToString("MMyyyy"))
		        {
			        _customAuthenticationStateProvider.UpdateAuthenticationState(userToLogin);
		        }
		        else
		        {
					throw new InvalidCredentialsException("Wrong email or password");
				}
			}
	        else
	        {
		        throw new InvalidCredentialsException("Wrong email or password");
			}
	        
        }

        public void Logout()
        {
	        _customAuthenticationStateProvider.UpdateAuthenticationState(null);
        }

    }
}
