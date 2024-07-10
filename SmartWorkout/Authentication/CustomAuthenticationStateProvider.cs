using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;
using SmartWorkout.DTO;
using SmartWorkout.Entities;

namespace SmartWorkout.Authentication;

public class CustomAuthenticationStateProvider:AuthenticationStateProvider
{
	// ProtectedSessionStorage for storing user session data securely in the browser.
	private readonly ProtectedSessionStorage _sessionStorage;

	// _anonymous for unautheticated user. a "claim" is a piece of information about a user or system entity.
	// ClaimsPrincipal is used to represent an anonymous (unauthenticated) user, and designed to work with claims-based identity systems
	// A ClaimsPrincipal can be composed of multiple ClaimsIdentity instances. 
	private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

	public CustomAuthenticationStateProvider(ProtectedSessionStorage sessionStorage)
	{
		_sessionStorage = sessionStorage;
	}
	public override async Task<AuthenticationState> GetAuthenticationStateAsync()
	{
		try
		{
			await Task.Delay(1000);
			var userSessionStorageResult = await _sessionStorage.GetAsync<UserDTO>("UserSession");
			var userSession = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;
			if (userSession == null)
				return await Task.FromResult(new AuthenticationState(_anonymous));
			var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
				{
					new Claim(ClaimTypes.Name, userSession.Email),
					new Claim(ClaimTypes.NameIdentifier, userSession.Id.ToString()),
					new Claim(ClaimTypes.Role, userSession.IsAdmin ? "Administrator" : "User")
				}, "CustomAuth"));
			return await Task.FromResult(new AuthenticationState(claimsPrincipal));
		}
		catch
		{
			return await Task.FromResult(new AuthenticationState(_anonymous));
		}
	}

	public void  UpdateAuthenticationState(UserDTO userSession)
	{
		ClaimsPrincipal claimsPrincipal;

		if (userSession != null)
		{
			 _sessionStorage.SetAsync("UserSession", userSession);
			claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
				{
					new(ClaimTypes.Name, userSession.Email),
					new Claim(ClaimTypes.NameIdentifier, userSession.Id.ToString()),
					new Claim(ClaimTypes.Role, userSession.IsAdmin ? "Administrator" : "User")
				}));
		}
		else
		{
			 _sessionStorage.DeleteAsync("UserSession");
			claimsPrincipal = _anonymous;
		}
		NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
	}
}	
