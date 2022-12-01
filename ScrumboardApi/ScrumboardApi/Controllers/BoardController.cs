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
        public IActionResult GetUsers()
        { 
            List<UserModel> result;
            try
            {
                result = _boardService.GetUsers();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

            return Ok(result);
        }

		[HttpGet("GetStates")]
		public IActionResult GetStates()
        {
            List<State> result;
            try
            {
                result = _boardService.GetStates();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
			return Ok(result);
		}

		[HttpPost("CreateState")]
		public async Task<IActionResult> CreateState([FromBody]StateModel state)
        {
            StateModel result;
            try
            {
                result = await _boardService.CreateState(state);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
			return Ok(result);
		}

		[HttpPost("CreateTask")]
		public async Task<IActionResult> CreateTask([FromBody]BoardTaskModel model)
        {
            BoardTaskModel result;
            try
            {
                result = await _boardService.CreateTask(model);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            return Ok(result);
		}

		[HttpGet("GetTasks")]
		public async Task<IActionResult> GetTasks()
        {
            var result = new List<BoardTaskModel>();
            try
            {
                result = _boardService.GetTasks();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
			return Ok(result);
		}

		[HttpPost("UpdateTask")]
		public async Task<IActionResult> UpdateTask([FromBody] BoardTaskModel model)
        {
            try
            { 
                await _boardService.UpdateTask(model);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
			return Ok();
		}

        [HttpPost("DeleteTask")]
        public async Task<IActionResult> DeleteTask([FromBody] BoardTaskModel task)
        {
            try
            {
                await _boardService.DeleteTask(task);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            return Ok();
        }
    }
}
