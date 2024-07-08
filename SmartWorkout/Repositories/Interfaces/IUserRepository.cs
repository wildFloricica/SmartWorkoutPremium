using SmartWorkout.DTO;
using SmartWorkout.Entities;

namespace SmartWorkout.Repositories.Interfaces;

public interface IUserRepository
{
	ICollection<User> GetUsers();

	void SaveUser(UserDTO user);

	UserDTO GetUserById(int? id);

	void UpdateUser(int? id,UserDTO user);

	void DeleteUser(int? id);
}