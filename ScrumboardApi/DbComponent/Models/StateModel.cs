using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbComponent.DbModels;

namespace DbComponent.Models
{
	public class StateModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int StatePriority { get; set; }
		public bool AcceptsTaskCreate { get; set; }


		public StateModel()
		{
		}

		public StateModel(State state)
		{
			Id = state.ID;
			Name = state.Name;
			StatePriority = state.StatePriority;
			AcceptsTaskCreate = state.AcceptsTaskCreate;
		}

		public static StateModel CreateModel(State task)
		{
			return new StateModel(task);
		}
	}
}
