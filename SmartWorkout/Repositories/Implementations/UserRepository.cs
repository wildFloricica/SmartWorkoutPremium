using Microsoft.EntityFrameworkCore;
using SmartWorkout.Context;
using SmartWorkout.DTO;
using SmartWorkout.Entities;
using SmartWorkout.Repositories.Interfaces;

namespace SmartWorkout.Repositories.Implementations
{
	public class UserRepository : IUserRepository
	{

		private readonly SmartWorkoutContext _context;

		public UserRepository(SmartWorkoutContext context)
		{
			_context = context;
		}

		public ICollection<User> GetUsers()
		{
			var users = _context.Users.ToList();

			return users;
		}

		public void SaveUser(UserDTO user)
		{
			User userToAdd = new User()
			{
				FirstName = user.FirstName,
				LastName = user.LastName,
				Birthday = user.Birthday,
				Gender = user.Gender,
				Email = user.Email
			};


			_context.Users.Add(userToAdd);
			_context.SaveChanges();
		}

		public void UpdateUser(int? id, UserDTO user)
		{
			User userToUpdate = _context.Users.FirstOrDefault(x => x.Id == id);

			if (userToUpdate != null)
			{
				userToUpdate.FirstName = user.FirstName;
				userToUpdate.LastName = user.LastName;
				userToUpdate.Birthday = user.Birthday;
				userToUpdate.Gender = user.Gender;
				userToUpdate.Email = user.Email;
				_context.Users.Update(userToUpdate);
				_context.SaveChanges();

			}
			else
			{
				throw new Exception($"User with id {id} not found!");
			}
		}

		public void DeleteUser(int? id)
		{
			User existingUser = _context.Users.FirstOrDefault(x => x.Id == id);

			if (existingUser != null)
			{
				_context.Users.Remove(existingUser);
				_context.SaveChanges();
			}
			else
			{
				throw new Exception("User not found!");
			}
		}

		public bool existsByEmail(string email)
		{
			User existingUser = _context.Users.FirstOrDefault(x => x.Email == email);

			if (existingUser != null)
			{
				return true;
			}
			return false;
		}

		public UserDTO GetUserByEmail(string email)
		{
			User existingUser = _context.Users.FirstOrDefault(x => x.Email == email);

			UserDTO existingUserDto = new UserDTO()
			{
				Id = existingUser.Id,
				Birthday = existingUser.Birthday,
				Email = existingUser.Email,
				FirstName = existingUser.FirstName,
				Gender = existingUser.Gender,
				LastName = existingUser.LastName,
				isAdmin = existingUser.IsAdmin
			};

			return existingUserDto;

		}

		public UserDTO GetUserById(int? id)
		{
			User existingUser = _context.Users.FirstOrDefault(x => x.Id == id);


			if (existingUser == null)
			{
				throw new Exception("User not found!");
			}


			UserDTO existingUserDto = new UserDTO()
			{
				Id = existingUser.Id,
				Birthday = existingUser.Birthday,
				FirstName = existingUser.FirstName,
				LastName = existingUser.LastName,
				Gender = existingUser.Gender,
				Email = existingUser.Email,
				isAdmin = existingUser.IsAdmin
			};
			return existingUserDto;
		}
	}
}
