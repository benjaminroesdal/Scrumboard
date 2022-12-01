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
