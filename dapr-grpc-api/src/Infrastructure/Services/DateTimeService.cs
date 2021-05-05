using SC.API.CleanArchitecture.Application.Common.Interfaces;
using System;

namespace SC.API.CleanArchitecture.Infrastructure.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
