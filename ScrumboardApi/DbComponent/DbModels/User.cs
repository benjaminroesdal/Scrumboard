using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbComponent.Models;

namespace DbComponent.DbModels
{
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public virtual ICollection<BoardTask> ReporterTasks { get; set; }
        public virtual ICollection<BoardTask> AssigneeTasks { get; set; }

        public User()
        {
        }

        public User(UserModel model)
        {
	        ID = model.Id;
            UserName = model.UserName;
            Email = model.Email;
        }

        public static User CreateDao(UserModel model)
        {
            return new User(model);
        }
    }

    public static class UserExtensions
    {
        public static List<User> CreateModelList(this ICollection<UserModel> modelList)
        {
            return modelList.Select(CreateDao).ToList();
        }

        public static User CreateDao(this UserModel model)
	    {
		    return User.CreateDao(model);
	    }
    }
}
