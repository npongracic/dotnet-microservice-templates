using SC.API.CleanArchitecture.Application.Users.Queries;
using MediatR;
using System.Collections.Generic;

namespace SC.API.CleanArchitecture.API.Security
{
    public class OperationsMapper
    {
        private readonly IMediator _mediator;
        private Dictionary<string, List<int>> _operationsMap;

        public OperationsMapper(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Dictionary<string, List<int>> OperationsMap
        {
            get
            {
                if (_operationsMap == null) {
                    _operationsMap = _mediator.Send(new UserOperationsQuery()).Result;
                }

                return _operationsMap;
            }
        }
    }
}
