namespace Scrumboard.Models
{

	public class BoardTask
	{
		public int TaskID { get; set; }
		public int StateID { get; set; }
		public int AssigneeID { get; set; }
		public int ReporterID { get; set; }
		public string name { get; set; }
		public string description { get; set; }
		public int originalEstimate { get; set; }
		public Priority priority { get; set; }
		public State state { get; set; }
		public User reporter { get; set; }
		public User assignee { get; set; }
	}

	public class Reporter
	{
		public int id { get; set; }
		public string userName { get; set; }
	}

	public class Assignee
	{
		public int id { get; set; }
		public string userName { get; set; }
	}

}
