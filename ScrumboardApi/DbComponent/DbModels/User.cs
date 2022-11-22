using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbComponent.DbModels
{
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }

        public virtual ICollection<BoardTask> ReporterTasks { get; set; }
        public virtual ICollection<BoardTask> AssigneeTasks { get; set; }
    }
}
