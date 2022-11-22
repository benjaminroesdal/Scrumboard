using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbComponent.DbModels
{
    public class BoardTask
    {
        [Key]
        public int TaskID { get; set; }
        public int StateID { get; set; }
        public int AssigneeID { get; set; }
        public int ReporterID { get; set; }
        public int Description { get; set; }
        public int OriginalEstimate { get; set; }
        public Priority Priority { get; set; }

        public State State { get; set; }
        public User Assignee { get; set; }
        public User Reporter { get; set; }

    }
}
