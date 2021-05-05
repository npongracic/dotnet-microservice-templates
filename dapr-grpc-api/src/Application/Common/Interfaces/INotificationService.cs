using MediatR;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.Application.Common.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync(INotification message);
    }
}
