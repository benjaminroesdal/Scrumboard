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
		public string Email { get; set; }

		public UserModel()
		{
		}

		//To ensure easy and safe mapping between dao and model.
		public UserModel(User user)
		{
			Id = user.ID;
			UserName = user.UserName;
			Email = user.Email;
		}

		public static UserModel CreateModel(User dao)
		{
			return new UserModel(dao);
		}
	}

	public static class UserModelExtensions
	{
		public static List<UserModel> CreateModelList(this ICollection<User> daoList)
		{
			return daoList.Select(CreateDao).ToList();
		}

		public static UserModel CreateDao(this User dao)
		{
			return UserModel.CreateModel(dao);
		}
	}
}
