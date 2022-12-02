using System.Net.Http.Json;
using Blazored.Toast.Services;
using Microsoft.JSInterop;
using RestSharp;
using Scrumboard.Models;
using static System.Net.WebRequestMethods;

namespace Scrumboard.Services
{
	public class BoardApiService
	{
		private static HttpClient _client;
        private IToastService _toastService;

		public BoardApiService(HttpClient client, IToastService toastService)
		{
			_client = client;
            _toastService = toastService;
        }

        /// <summary>
        /// Gets users from API - in case of error codes from the backend, a toast will be shown describing the error.
        /// </summary>
        /// <returns>Returns list of users</returns>
        public async Task<List<User>> GetUsers()
        {
            List<User> users = new List<User>();
            try
            {
                users = await _client.GetFromJsonAsync<List<User>>("https://c7bf-93-176-82-58.eu.ngrok.io/api/Board/GetUsers");
            }
            catch (Exception e)
            {
                _toastService.ShowError($"Error occurred when trying to fetch users - {e.Message}");
            }

            return users;
        }

        /// <summary>
        /// Gets states from DB and shows toast in case of error codes.
        /// </summary>
        /// <returns>List of states</returns>
		public async Task<List<State>> GetSections()
        {
            List<State> states = new List<State>();
            try
            {
                states = await _client.GetFromJsonAsync<List<State>>("https://c7bf-93-176-82-58.eu.ngrok.io/api/Board/GetStates");
            }
            catch (Exception e)
            {
                _toastService.ShowError($"Error occurred when trying to fetch states - {e.HResult}");
            }
            return states;
        }

        /// <summary>
        /// Gets tasks from api and shows toast with error information.
        /// </summary>
        /// <returns>List of tasks.</returns>
        public async Task<List<BoardTask>> GetTasks()
        {
            List<BoardTask> tasks = new List<BoardTask>();
            try
            {
                tasks = await _client.GetFromJsonAsync<List<BoardTask>>("https://c7bf-93-176-82-58.eu.ngrok.io/api/Board/GetTasks");
            }
            catch (Exception e)
            {
                _toastService.ShowError($"Error occurred when trying to fetch tasks - {e.Message}");
            }
            return tasks;
        }

        /// <summary>
        /// Call backend to create a task and waits to receive saved task as response to ensure data integrity in frontend
        /// Shows toast with error information in case of error codes.
        /// </summary>
        /// <param name="task"></param>
        /// <returns>Task saved to DB returned by API.</returns>
        public async Task<BoardTask> CreateTask(BoardTask task)
        {
            BoardTask taskResult = new BoardTask();
            try
            {
                var result = await _client.PostAsJsonAsync<BoardTask>("https://c7bf-93-176-82-58.eu.ngrok.io/api/Board/CreateTask", task);
                taskResult = await result.Content.ReadFromJsonAsync<BoardTask>();
            }
            catch (Exception e)
            {
                _toastService.ShowError($"Error occurred when trying to create task {task.name} - {e.Message}");
            }
            return taskResult;
        }

        /// <summary>
        /// Calls API to update provided task
        /// </summary>
        /// <param name="task">Task to update</param>
        public async Task UpdateTask(BoardTask task)
        {
            try
            {
                await _client.PostAsJsonAsync<BoardTask>("https://c7bf-93-176-82-58.eu.ngrok.io/api/Board/UpdateTask", task);
            }
            catch (Exception e)
            {
                _toastService.ShowError($"Error occurred when trying to update task {task.name} - {e.Message}");
            }
        }

        /// <summary>
        /// Calls API to delete provided task
        /// </summary>
        /// <param name="task">Task to delete</param>
        public async Task DeleteTask(BoardTask task)
        {
            try
            {
                await _client.PostAsJsonAsync("https://c7bf-93-176-82-58.eu.ngrok.io/api/Board/DeleteTask", task);
            }
            catch (Exception e)
            {
                _toastService.ShowError($"Error occurred when trying to delete task {task.name} - {e.Message}");
            }
        }

        /// <summary>
        /// Calls api to create provided state
        /// </summary>
        /// <param name="state">State to create</param>
        /// <returns>Created state returned from backend to ensure data integrity in frontend.</returns>
        public async Task<State> CreateState(State state)
        {
            State stateRes = new State();
            try
            {
                var result = await _client.PostAsJsonAsync<State>("https://c7bf-93-176-82-58.eu.ngrok.io/api/Board/CreateState", state);
                stateRes = await result.Content.ReadFromJsonAsync<State>();
            }
            catch (Exception e)
            {
                _toastService.ShowError($"Error occurred when trying to create new state {state.Name} - {e.Message}");
            }
            return stateRes;
        }
	}
}
