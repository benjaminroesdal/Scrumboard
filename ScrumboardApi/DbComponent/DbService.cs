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

		public async Task<State> CreateState(State state)
		{
			_context.Add(state);
			var result = await _context.SaveChangesAsync();

			if (result == 0)
				throw new Exception("Nothing was saved to db");
			return state;
		}

        public List<State> GetStates()
		{
			var listResult = _context.States.OrderByDescending(e => e.StatePriority).ToList();
			return listResult;
		}

		public async Task<BoardTask> CreateTask(BoardTask task)
		{
			await _context.Tasks.AddAsync(task);
			var saveResult = await _context.SaveChangesAsync();
			if (saveResult == 0)
				throw new Exception("Nothing was saved to db");
			var taskDao = await _context.Tasks.Include(e => e.Reporter)
				.Include(e => e.Assignee)
				.Include(e => e.State)
				.FirstOrDefaultAsync(e => e.TaskID == task.TaskID);
			return taskDao;
		}

		public async Task UpdateTask(BoardTask task)
		{
			var currentTask = _context.Tasks.FirstOrDefault(e => e.TaskID == task.TaskID);
			MapChanged(task, currentTask);
			_context.Update(currentTask);
			var result = await _context.SaveChangesAsync();
			if (result == 0)
				throw new Exception("Nothing was saved to db");
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

		public List<BoardTask> GetTasks()
		{
			var result = _context.Tasks.Include("State").Include("Assignee").Include("Reporter").ToList();
			return result;
		}

		public List<User> GetUsers()
		{
			return _context.Users.ToList();
		}

		public async Task DeteleTask(BoardTask task)
        {
	        _context.Tasks.Remove(task);
	        var result = await _context.SaveChangesAsync();
	        if (result == 0)
		        throw new Exception("Nothing was saved to db");
		}
    }
}
