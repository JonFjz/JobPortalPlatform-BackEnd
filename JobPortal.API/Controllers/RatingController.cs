using JobPortal.API.Controllers.Base;
using JobPortal.Application.Features.Ratings.Commands.CreateRating;
using JobPortal.Application.Features.Ratings.Commands.DeleteRating;
using JobPortal.Application.Features.Ratings.Commands.UpdateRating;
using JobPortal.Application.Features.Ratings.Queries.GetRatingByEmployerId;
using JobPortal.Application.Helpers.Models.Cashe;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers
{
    [Authorize]
    public class RatingController : BaseApiController
    {
        private readonly IMediator _mediator;
        public RatingController(IMediator mediator)
        {
            _mediator = mediator;            
        }


        [Authorize(Policy = "JobSeeker")]
        [HttpPost]
        public async Task<IActionResult> AddRating([FromBody] CreateRatingCommand command)
        {
             await _mediator.Send(command);
            return Ok();
        }

        [Authorize(Policy = "JobSeeker")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateRatingCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }


        [HttpGet("employer/{employerId}")]
        public async Task<IActionResult> GetRatingByEmployerId([FromRoute] int employerId)
        {
            var rating = await _mediator.Send(new GetRatingByEmployerIdQuery(employerId));
            return Ok(rating);
        }


        [Authorize(Policy = "JobSeeker")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteRatingCommand(id));
            return NoContent();
        }
    }
}
