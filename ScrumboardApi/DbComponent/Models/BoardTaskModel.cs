using DbComponent.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbComponent.Models
{
	public class BoardTaskModel
	{
		public string Name { get; set; }
		public int TaskID { get; set; }
		public int StateID { get; set; }
		public int AssigneeID { get; set; }
		public int ReporterID { get; set; }
		public string Description { get; set; }
		public int OriginalEstimate { get; set; }
		public Priority Priority { get; set; }
		public StateModel? State { get; set; }
		public UserModel? Reporter { get; set; }
		public UserModel? Assignee { get; set; }

		public BoardTaskModel()
		{
		}

		private BoardTaskModel(BoardTask task)
		{
			Name = task.Name;
			TaskID = task.TaskID;
			StateID = task.StateID;
			AssigneeID = task.AssigneeID;
			ReporterID = task.ReporterID;
			Description = task.Description;
			OriginalEstimate = task.OriginalEstimate;
			Priority = task.Priority;
			State = StateModel.CreateModel(task?.State);
			Assignee = UserModel.CreateModel(task.Assignee);
			Reporter = UserModel.CreateModel(task.Reporter);
		}

		public static BoardTaskModel CreateModel(BoardTask task)
		{
			return new BoardTaskModel(task);
		}
	}

	public static class BoardTaskModelExtensions
	{
		public static List<BoardTaskModel> CreateModelList(this ICollection<BoardTask> modelList)
		{
			return modelList.Select(CreateModel).ToList();
		}

		public static BoardTaskModel CreateModel(this BoardTask task)
		{
			return BoardTaskModel.CreateModel(task);
		}
	}
}
