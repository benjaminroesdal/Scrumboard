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

		/// <summary>
		/// This is created strictly as an easy and safe way to ensure correct DAO/Model creation before the dao object and model object.
		/// There is also an empty constructor to ensure the created of this model is not dependent on the dao model.
		/// </summary>
		/// <param name="dao">BoardTask dao model.</param>
		private BoardTaskModel(BoardTask dao)
		{
			Name = dao.Name;
			TaskID = dao.TaskID;
			StateID = dao.StateID;
			AssigneeID = dao.AssigneeID;
			ReporterID = dao.ReporterID;
			Description = dao.Description;
			OriginalEstimate = dao.OriginalEstimate;
			Priority = dao.Priority;
			State = StateModel.CreateModel(dao?.State);
			Assignee = UserModel.CreateModel(dao.Assignee);
			Reporter = UserModel.CreateModel(dao.Reporter);
		}

		public static BoardTaskModel CreateModel(BoardTask task)
		{
			return new BoardTaskModel(task);
		}
	}

	public static class BoardTaskModelExtensions
	{
		public static List<BoardTaskModel> CreateModelList(this ICollection<BoardTask> daoList)
		{
			return daoList.Select(CreateModel).ToList();
		}

		public static BoardTaskModel CreateModel(this BoardTask dao)
		{
			return BoardTaskModel.CreateModel(dao);
		}
	}
}
