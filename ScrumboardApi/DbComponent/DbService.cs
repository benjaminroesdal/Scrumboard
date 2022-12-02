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

		/// <summary>
		/// Adds new state to db and returns state with populated ID from EF.
		/// </summary>
		/// <param name="state">State to add to DB</param>
		public async Task<State> CreateState(State state)
		{
			_context.Add(state);
			var result = await _context.SaveChangesAsync();

			if (result == 0)
				throw new Exception("Nothing was saved to db");
			return state;
		}

		/// <summary>
		/// Gets list from database and orders by priority descending.
		/// </summary>
		/// <returns>Ordered list of states </returns>
        public List<State> GetStates()
		{
			var listResult = _context.States.OrderByDescending(e => e.StatePriority).ToList();
			return listResult;
		}

		/// <summary>
		/// Saves provided task to DB and returns saved task back to caller.
		/// </summary>
		/// <param name="task">Task to save to DB</param>
		/// <returns></returns>
		/// <exception cref="Exception">Exception to be thrown in case of issues saving to DB.</exception>
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

		/// <summary>
		/// Finds task to update from DB, modifies values to provided task and saves task.
		/// </summary>
		/// <param name="task">Task to update.</param>
		/// <exception cref="Exception">Exception if error saving.</exception>
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

		/// <summary>
		/// Gets tasks and includes sub objects aswell
		/// </summary>
		/// <returns>Returns list of tasks</returns>
		public List<BoardTask> GetTasks()
		{
			var result = _context.Tasks.Include("State").Include("Assignee").Include("Reporter").ToList();
			return result;
		}

		/// <summary>
		/// Gets users from db
		/// </summary>
		/// <returns>List of users from DB</returns>
		public List<User> GetUsers()
		{
			return _context.Users.ToList();
		}

		/// <summary>
		/// Deletes provided task from DB and saves changes.
		/// </summary>
		/// <param name="task"></param>
		/// <returns></returns>
		/// <exception cref="Exception">If saved count is 0 exception is thrown.</exception>
		public async Task DeteleTask(BoardTask task)
        {
	        _context.Tasks.Remove(task);
	        var result = await _context.SaveChangesAsync();
	        if (result == 0)
		        throw new Exception("Nothing was saved to db");
		}
    }
}
