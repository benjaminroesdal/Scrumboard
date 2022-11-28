using BoardComponent;
using DbComponent;
using DbComponent.DbModels;
using DbComponent.Models;
using Microsoft.AspNetCore.Mvc;

namespace ScrumboardApi.Controllers
{
	[Route("api/Board")]
	public class BoardController : Controller
	{
		private readonly IBoardService _boardService;
		public BoardController(IBoardService boardService)
		{
			_boardService = boardService;
		}

        [HttpGet("GetUsers")]
        public List<UserModel> GetUsers()
        {
			var result = _boardService.GetUsers();
            return result;
        }

		[HttpGet("GetStates")]
		public List<State> GetStates()
		{
			var result = _boardService.GetStates();
			return result;
		}

		[HttpPost("CreateState")]
		public async Task<StateModel> CreateState([FromBody]StateModel state)
		{
			var result = await _boardService.CreateState(state);
			return result;
		}

		[HttpPost("CreateTask")]
		public async Task<IActionResult> CreateTask([FromBody]BoardTaskModel model)
		{
			var result = await _boardService.CreateTask(model);
			return Ok(result);
		}

		[HttpGet("GetTasks")]
		public List<BoardTaskModel> GetTasks()
		{
			return _boardService.GetTasks();
		}

		[HttpPost("UpdateTask")]
		public async Task<IActionResult> UpdateTask([FromBody] BoardTaskModel model)
		{
			await _boardService.UpdateTask(model);
			return Ok();
		}

        [HttpPost("DeleteTask")]
        public async Task<IActionResult> DeleteTask([FromBody] BoardTaskModel task)
        {
            await _boardService.DeleteTask(task);
            return Ok();
        }
    }
}
