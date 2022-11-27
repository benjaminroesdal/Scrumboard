using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbComponent.Models;

namespace DbComponent.DbModels
{
    public class BoardTask
    {
        [Key]
        public int TaskID { get; set; }
        public string Name { get; set; }
        public int StateID { get; set; }
        public int AssigneeID { get; set; }
        public int ReporterID { get; set; }
        public string Description { get; set; }
        public int OriginalEstimate { get; set; }
        public Priority Priority { get; set; }

        public State? State { get; set; }
        public User? Assignee { get; set; }
        public User? Reporter { get; set; }

        public BoardTask()
        {
        }

        public BoardTask(BoardTaskModel model)
        {
            Name = model.Name;
            TaskID = model.TaskID;
	        StateID = model.StateID;
            AssigneeID = model.AssigneeID;
            ReporterID = model.ReporterID;
            Description = model.Description;
            OriginalEstimate = model.OriginalEstimate;
            Priority = model.Priority;
        }

        public static BoardTask CreateDao(BoardTaskModel model)
        {
	        return new BoardTask(model);
        }
    }

    public static class BoardTasksExtensions {
	    public static List<BoardTask> CreateModelList(this ICollection<BoardTaskModel> modelList)
	    {
		    return modelList.Select(CreateDao).ToList();
	    }

		public static BoardTask CreateDao(this BoardTaskModel model)
	    {
            return BoardTask.CreateDao(model);
	    }
    }
}
