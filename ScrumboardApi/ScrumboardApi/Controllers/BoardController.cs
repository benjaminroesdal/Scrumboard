using DbComponent;
using DbComponent.DbModels;
using DbComponent.Models;
using Microsoft.AspNetCore.Mvc;

namespace ScrumboardApi.Controllers
{
	[Route("api/Board")]
	public class BoardController : Controller
	{
		private readonly IDbservice _dbService;
		public BoardController(IDbservice dbService)
		{
			_dbService = dbService;
		}

        [HttpGet("GetUsers")]
        public List<UserModel> GetUsers()
        {
			var result = _dbService.GetUsers();
            return result;
        }

		[HttpGet("GetStates")]
		public List<State> GetStates()
		{
			var result = _dbService.GetStates();
			return result;
		}

		[HttpPost("CreateState")]
		public IActionResult CreateState([FromBody]StateModel state)
		{
			_dbService.CreateState(state);
			return Ok();
		}

		[HttpPost("CreateTask")]
		public async Task<IActionResult> CreateTask([FromBody]BoardTaskModel model)
		{
			var result = await _dbService.CreateTask(model);
			return Ok(result);
		}

		[HttpGet("GetTasks")]
		public List<BoardTaskModel> GetTasks()
		{
			return _dbService.GetTasks();
		}

		[HttpPost("UpdateTask")]
		public async Task<IActionResult> UpdateTask([FromBody] BoardTaskModel model)
		{
			await _dbService.UpdateTask(model);
			return Ok();
		}

        [HttpPost("DeleteTask")]
        public async Task<IActionResult> DeleteTask([FromBody] BoardTaskModel task)
        {
            await _dbService.DeteleTask(task);
            return Ok();
        }
    }
}
