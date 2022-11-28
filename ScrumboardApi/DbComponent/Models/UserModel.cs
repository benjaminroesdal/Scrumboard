using DbComponent.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbComponent.Models
{
	public class UserModel
	{
		public int Id { get; set; }
		public string UserName { get; set; }

		public UserModel()
		{
		}

		public UserModel(User user)
		{
			Id = user.ID;
			UserName = user.UserName;
		}

		public static UserModel CreateModel(User user)
		{
			return new UserModel(user);
		}
	}

	public static class UserModelExtensions
	{
		public static List<UserModel> CreateModelList(this ICollection<User> modelList)
		{
			return modelList.Select(CreateDao).ToList();
		}

		public static UserModel CreateDao(this User model)
		{
			return UserModel.CreateModel(model);
		}
	}
}
