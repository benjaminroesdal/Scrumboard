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
        Task<State> CreateState(State state);
		List<State> GetStates();
		Task<BoardTask> CreateTask(BoardTask task);
		List<User> GetUsers();
		List<BoardTask> GetTasks();
		Task UpdateTask(BoardTask task);
		Task DeteleTask(BoardTask task);


	}
}
