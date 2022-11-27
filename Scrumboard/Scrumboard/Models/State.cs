namespace Scrumboard.Models
{

	public class StateResponse
	{
		public State[] States { get; set; }
	}

	public class State
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int StatePriority { get; set; }
		public bool AcceptsTaskCreate { get; set; }
		public bool NewTaskOpen { get; set; }
		public string NewTaskName { get; set; }
	}

}
