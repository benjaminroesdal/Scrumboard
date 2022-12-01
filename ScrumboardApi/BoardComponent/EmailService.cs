using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DbComponent.Models;

namespace BoardComponent
{
	public class EmailService
	{
		public async Task SendEmail(BoardTaskModel task)
		{
			var smtpClient = new SmtpClient("smtp-mail.outlook.com")
			{
				Port = 587,
				Credentials = new NetworkCredential("####", "####"),
				EnableSsl = true,
			};

			await smtpClient.SendMailAsync("benjaminroesdal@hotmail.com", task.Assignee.Email, "Scrumboard", $"Hello {task.Assignee.UserName} you have been assigned Task: {task.Name}");
		}
	}
}
