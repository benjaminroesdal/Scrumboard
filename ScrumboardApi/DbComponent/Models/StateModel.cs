using DbComponent.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		/// <summary>
		/// To ensure safe and easy mapping between dao and model.
		/// </summary>
		/// <param name="state"></param>
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

	public static class StateModelExtensions
	{
		public static StateModel CreateModel(this State dao)
		{
			return StateModel.CreateModel(dao);
		}
	}
}
