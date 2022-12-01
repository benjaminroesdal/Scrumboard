using DbComponent.DbModels;
using DbComponent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbComponent;

namespace BoardComponent
{
	public class BoardService : IBoardService
	{
		private readonly IDbservice _dbService;
		private readonly EmailService _emailService;

		public BoardService(IDbservice dbService)
		{
			_dbService = dbService;
			_emailService = new EmailService();
		}
		public List<UserModel> GetUsers()
		{
			var result = _dbService.GetUsers();
			return result.CreateModelList();
		}

		public List<State> GetStates()
		{
			var result = _dbService.GetStates();
			return result;
		}

		public async Task<StateModel> CreateState(StateModel state)
		{
			var result = await _dbService.CreateState(state.CreateDao());
			return result.CreateModel();
		}

		public async Task<BoardTaskModel> CreateTask(BoardTaskModel model)
		{
			var result = await _dbService.CreateTask(model.CreateDao());
			var taskModel = result.CreateModel();
			await _emailService.SendEmail(taskModel);
			return result.CreateModel();
		}

		public List<BoardTaskModel> GetTasks()
		{
			var result = _dbService.GetTasks().CreateModelList();
			return result;
		}

		public async Task UpdateTask(BoardTaskModel model)
		{
			await _dbService.UpdateTask(model.CreateDao());
		}

		public async Task DeleteTask(BoardTaskModel task)
		{
			await _dbService.DeteleTask(task.CreateDao());
		}
	}
}
