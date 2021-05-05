using System;
using System.Threading.Tasks;
using SC.API.CleanArchitecture.Application.CatalogItems.Commands;
using SC.API.CleanArchitecture.Application.CatalogItems.Queries;
using SC.API.CleanArchitecture.Application.Catalogs.Queries;
using SC.API.CleanArchitecture.Application.Common;
using SC.API.CleanArchitecture.Application.Common.Specifications;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SC.API.CleanArchitecture.API.Controllers
{
    [Route("api/catalogs")]
    [ApiController]
    public class CatalogController : Controller
    {
        private readonly IMediator _mediator;

        public CatalogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PageableCollection<CatalogDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCatalogs(
            Guid? catalogId,
            DateTimeOffset? dateFrom,
            DateTimeOffset? dateTo,
            string systemName,
            string userFriendlyName,
            [FromQuery] QueryOptions queryOptions,
            bool onlyActiveRecords = true)
        {
            var specification = new CatalogSpecification(
               catalogId,
               dateFrom,
               dateTo,
               systemName,
               userFriendlyName,
               onlyActiveRecords
           );

            return Ok(await _mediator.Send(new GetCatalogsQuery(specification, queryOptions)));
        }

        [HttpGet("items")]
        [ProducesResponseType(typeof(PageableCollection<CatalogItemDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCatalogItems(
            Guid? catalogId,
            Guid? catalogItemId,
            DateTimeOffset? dateFrom,
            DateTimeOffset? dateTo,
            string systemName,
            string userFriendlyName,
            string value,
            [FromQuery] QueryOptions queryOptions,
            bool onlyActiveRecords = true)
        {
            var specification = new CatalogItemSpecification(
               catalogId,
               catalogItemId,
               dateFrom,
               dateTo,
               systemName,
               userFriendlyName,
               value,
               onlyActiveRecords
           );

            return Ok(await _mediator.Send(new GetCatalogItemsQuery(specification, queryOptions)));
        }

        [HttpPost("items")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateCatalogItem(CreateCatalogItemCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("items")]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteCatalogItem(DeleteCatalogItemCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        [HttpDelete("items/{catalogItemId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteCatalogItem(Guid catalogItemId)
        {
            await _mediator.Send(new DeleteCatalogItemCommand(catalogItemId));
            return NoContent();
        }

        [HttpPut("items/activate")]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
        public async Task<IActionResult> ActivateCatalogItem(ActivateCatalogItemCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        [HttpPut("items")]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCatalogItem(UpdateCatalogItemCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }
    }
}