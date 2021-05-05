using SC.API.CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        public Task SendAsync(INotification message)
        {
            return Task.CompletedTask;
        }
    }
}
