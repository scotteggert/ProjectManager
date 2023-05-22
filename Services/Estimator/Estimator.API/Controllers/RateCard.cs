using System;
using System.Net;
using System.Threading.Tasks;
using Estimator.Application.Commands;

using Estimator.Application.Commands;
using Estimator.Application.Queries;
using Estimator.Application.Models;
using Estimator.Domain.AggregatesModel.RateCard;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Estimator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateCardsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IRateCardQueries _rateCardQueries;

        public RateCardsController(IMediator mediator, IRateCardQueries rateCardQueries)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _rateCardQueries = rateCardQueries ?? throw new ArgumentNullException(nameof(rateCardQueries));
        }

        [HttpPost]
        [ProducesResponseType(typeof(RateCardDto), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateRateCardAsync([FromBody] CreateRateCardCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetRateCardByIdAsync), new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RateCardDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetRateCardByIdAsync(Guid id)
        {
            var rateCard = await _rateCardQueries.GetRateCardByIdAsync(id);

            if (rateCard == null)
            {
                return NotFound();
            }

            return Ok(rateCard);
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedItemsDto<RateCardDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetRateCardsAsync([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var rateCards = await _rateCardQueries.GetRateCardsAsync(pageSize, pageIndex);

            var totalItems = rateCards.TotalCount;
            var totalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);

            var model = new PaginatedItemsDto<RateCardDto>(pageIndex, pageSize, totalPages, rateCards.Items);

            return Ok(model);
        }

        [HttpPost("{id}/items")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> AddRateCardItemAsync(Guid id, [FromBody] AddRateCardItemCommand command)
        {
            command.RateCardId = id;

            await _mediator.Send(command);

            return CreatedAtAction(nameof(GetRateCardByIdAsync), new { id }, null);
        }
    }
}