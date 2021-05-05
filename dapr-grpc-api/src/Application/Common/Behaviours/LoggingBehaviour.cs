using SC.API.CleanArchitecture.Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.Application.Common.Behaviours
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly ICurrentUserService _currentUserService;

        public LoggingBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var requestName = typeof(TRequest).Name;
            var userId = _currentUserService.UserId ?? string.Empty;
            var userName = _currentUserService.Username;

            _logger.LogInformation("eLegal Request: {Name} {@UserId} {@UserName} {@Request}",
                requestName, userId, userName, request);
        }
    }
}
