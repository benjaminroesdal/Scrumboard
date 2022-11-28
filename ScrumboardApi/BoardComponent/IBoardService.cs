using DbComponent.DbModels;
using DbComponent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardComponent
{
	public interface IBoardService
	{
		List<UserModel> GetUsers();
		List<State> GetStates();
		Task<StateModel> CreateState(StateModel state);
		Task<BoardTaskModel> CreateTask(BoardTaskModel model);
		List<BoardTaskModel> GetTasks();
		Task UpdateTask(BoardTaskModel model);
		Task DeleteTask(BoardTaskModel task);
	}
}
