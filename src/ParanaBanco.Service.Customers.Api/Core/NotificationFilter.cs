using Microsoft.AspNetCore.Mvc.Filters;
using ParanaBanco.Service.Customers.Application.Core;

namespace ParanaBanco.Service.Customers.Api.Core
{
    public class NotificationFilter : IAsyncResultFilter
	{
		private readonly INotificationContext _notificationContext;

		public NotificationFilter(INotificationContext notificationContext)
		{
			_notificationContext = notificationContext;
		}

		public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
		{
			if (_notificationContext.HasNotifications)
			{
				context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
				context.HttpContext.Response.ContentType = "application/json";

				await context.HttpContext.Response.WriteAsJsonAsync(_notificationContext.Notifications);

				return;
			}

			await next();
		}
	}
}
