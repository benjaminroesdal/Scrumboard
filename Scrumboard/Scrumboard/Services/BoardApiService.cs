using System.Net.Http.Json;
using RestSharp;
using Scrumboard.Models;
using static System.Net.WebRequestMethods;

namespace Scrumboard.Services
{
	public class BoardApiService
	{
		private static HttpClient _client;

		public BoardApiService(HttpClient client)
		{
			_client = client;
		}

        public async Task<List<User>> GetUsers()
        {
            var result = await _client.GetFromJsonAsync<List<User>>("https://localhost:7209/api/Board/GetUsers");
            return result;
        }

		public async Task<List<State>> GetSections()
		{
			var result = await _client.GetFromJsonAsync<List<State>>("https://localhost:7209/api/Board/GetStates");
            return result;
        }

        public async Task<List<BoardTask>> GetTasks()
        {
            var tasks = await _client.GetFromJsonAsync<List<BoardTask>>("https://localhost:7209/api/Board/GetTasks");
            return tasks;
        }

        public async Task CreateTask(BoardTask task)
        {
            await _client.PostAsJsonAsync<BoardTask>("https://localhost:7209/api/Board/CreateTask", task);

        }

        public async Task UpdateTask(BoardTask task)
        {
            await _client.PostAsJsonAsync<BoardTask>("https://localhost:7209/api/Board/UpdateTask", task);
        }

        public async Task CreateState(State state)
        {
            await _client.PostAsJsonAsync("https://localhost:7209/api/Board/CreateState", state);
        }
    }
}
