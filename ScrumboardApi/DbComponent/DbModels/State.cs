using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbComponent.Models;

namespace DbComponent.DbModels
{
    public class State
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int StatePriority { get; set; }
        public bool AcceptsTaskCreate { get; set; }

        public virtual ICollection<BoardTask> Tasks { get; set; }

        public State()
        {
        }

        public State(StateModel model)
        {
	        ID = model.Id;
            Name = model.Name;
            StatePriority = model.StatePriority;
            AcceptsTaskCreate = model.AcceptsTaskCreate;
        }

        public static State CreateDao(StateModel model)
        {
	        return new State(model);
        }
    }

    public static class StateExtensions
    {
	    public static State CreateDao(this StateModel model)
	    {
		    return State.CreateDao(model);
	    }
    }
}
