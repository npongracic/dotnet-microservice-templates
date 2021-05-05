using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SC.API.CleanArchitecture.Application.Common;
using SC.API.CleanArchitecture.Application.Common.Specifications;
using SC.API.CleanArchitecture.Application.Organization.Queries.Officials;
using SC.API.CleanArchitecture.Application.Organization.Queries.Organizations;
using SC.API.CleanArchitecture.Application.Organization.Queries.OrganizationsOfficials;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SC.API.CleanArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrganizationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("organizations")]
        [ProducesResponseType(typeof(PageableCollection<OrganizationDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrganizations(
            string organizationIdentificationNumber,
            string organizationTradingName,
            long? partyRoleID,
            string fullName,
            [FromQuery] QueryOptions queryOptions,
            int? level = null,
            bool onlyActiveRecords = true
        )
        {
            var specification = new OrganizationSpecification(
                organizationIdentificationNumber,
                organizationTradingName,
                partyRoleID,
                level,
                onlyActiveRecords
           );

            return Ok(await _mediator.Send(new GetOrganizationsQuery(specification, queryOptions)));
        }

        [HttpGet("officials")]
        [ProducesResponseType(typeof(PageableCollection<OfficialDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOfficials(
            string code,
            string name,
            long? id,
            [FromQuery] QueryOptions queryOptions,
            bool onlyActiveRecords = true
        )
        {
            var specification = new OfficialSpecification(code,
                name,
                id,
                onlyActiveRecords
           );

            return Ok(await _mediator.Send(new GetOrganizationOfficialsQuery(specification, queryOptions)));
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(PageableCollection<OrganizationOfficialDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrganizationsOfficials(
            string code,
            string name,
            long? id,
            [FromQuery] QueryOptions queryOptions,
            bool onlyActiveRecords = true
        )
        {
            var specification = new EntireOrganizationSpecification(
                code,
                name,
                id,
                onlyActiveRecords
           );

            return Ok(await _mediator.Send(new GetEntireOrganizationQuery(specification, queryOptions)));
        }
    }
}
