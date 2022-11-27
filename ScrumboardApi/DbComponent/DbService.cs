using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbComponent.Context;
using DbComponent.DbModels;
using DbComponent.Models;
using Microsoft.EntityFrameworkCore;

namespace DbComponent
{
	public class DbService : IDbservice
	{
		private readonly ScrumBoardContext _context;
		public DbService(ScrumBoardContext context)
		{
			_context = context;
		}

		public async Task CreateState(StateModel state)
		{
			using (var context = _context)
			{
				var dao = state.CreateDao();
                context.Add(dao);
				var result = await context.SaveChangesAsync();
				if (result == 0)
					throw new Exception("Nothing was saved to db");
			}
		}

        public List<State> GetStates()
		{
			using var context = _context;
			var listResult = context.States.OrderByDescending(e => e.StatePriority).ToList();
			return listResult;
		}

		public async Task CreateTask(BoardTaskModel task)
		{
			using (var context = _context)
			{
				var dao = task.CreateDao();
				await context.Tasks.AddAsync(dao);
				var saveResult = await context.SaveChangesAsync();
				if (saveResult == 0) 
					throw new Exception("Nothing was saved to db");
			}
		}

		public async Task UpdateTask(BoardTaskModel task)
		{
			var dao = task.CreateDao();
			using (var context = _context)
			{
				var currentTask = context.Tasks.FirstOrDefault(e => e.TaskID == dao.TaskID);
				MapChanged(dao, currentTask);
				context.Update(currentTask);
				var result = await context.SaveChangesAsync();
				if (result == 0)
					throw new Exception("Nothing was saved to db");
			}
		}

		public void MapChanged(BoardTask updatedTask, BoardTask currentTask)
		{
			currentTask.Description = updatedTask.Description;
			currentTask.Name = updatedTask.Name;
			currentTask.AssigneeID = updatedTask.AssigneeID;
			currentTask.ReporterID = updatedTask.ReporterID;
			currentTask.Priority = updatedTask.Priority;
			currentTask.OriginalEstimate = updatedTask.OriginalEstimate;
			currentTask.StateID = updatedTask.StateID;
		}

		public List<BoardTaskModel> GetTasks()
		{
			using (var context = _context)
			{
				var result = context.Tasks.Include("State").Include("Assignee").Include("Reporter").ToList();
				var temp = result.CreateModelList();
				return temp;
			}
		}

		public List<UserModel> GetUsers()
		{
			using (var context = _context)
			{
				return context.Users.ToList().CreateModelList();
			}
		}
	}
}
