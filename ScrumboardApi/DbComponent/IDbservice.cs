using DbComponent.DbModels;
using DbComponent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbComponent
{
	public interface IDbservice
	{
        Task CreateState(StateModel state);
		List<State> GetStates();
		Task<BoardTaskModel> CreateTask(BoardTaskModel task);
		List<UserModel> GetUsers();
		List<BoardTaskModel> GetTasks();
		Task UpdateTask(BoardTaskModel task);
		Task DeteleTask(BoardTaskModel task);


	}
}
