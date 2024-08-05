using JobPortal.API.Controllers.Base;
using JobPortal.Application.Features.Reviews.Commands.CreateReview;
using JobPortal.Application.Features.Reviews.Commands.DeleteReview;
using JobPortal.Application.Features.Reviews.Commands.UpdateReview;
using JobPortal.Application.Features.Reviews.Queries.GetAllReview;
using JobPortal.Application.Features.Reviews.Queries.GetReviewById;
using JobPortal.Application.Helpers.Models.Cashe;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers
{

    public class ReviewController :BaseApiController
    {
        private readonly IMediator _mediator;
        public ReviewController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Policy = "JobSeeker")]
        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] CreateReviewCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new {ReviewId=result});
        }

        [Authorize(Policy = "JobSeeker")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateReviewCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var review = await _mediator.Send(new GetReviewByIdQuery(id));
            return Ok(review);
        }


        [HttpGet("employer/{employerId}")]
        //[Cached(1200)]
        public async Task<IActionResult> GetAll([FromRoute]int employerId,[FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var reviews = await _mediator.Send(new GetAllReviewQuery(employerId,pageNumber,pageSize));
            return Ok(reviews);
        }


        [Authorize(Policy = "JobSeeker")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteReviewCommand(id));
            return NoContent();
        }

    }
}
