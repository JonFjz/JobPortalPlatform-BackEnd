using JobPortal.Application.Features.BookmarkJobs.Commands.CreateBookmarkJob;
using JobPortal.Application.Features.BookmarkJobs.Commands.RemoveBookmarkJob;
using JobPortal.Application.Features.BookmarkJobs.Queries;
using JobPortal.Application.Helpers.Models.Cashe;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers
{
    [Authorize(Policy = "JobSeeker")]
    [ApiController]
    [Authorize]
    public class BookmarkJobsController : ControllerBase
    {

        private readonly IMediator _mediator;
        public BookmarkJobsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("bookmarks")]
        public async Task<IActionResult> AddBookmark([FromBody] CreateBookmarkJobCommand command)
        { 
            var result = await _mediator.Send(command);
            return Ok(new { JobPostingId = result });
        }


        [HttpDelete("bookmarks/{jobPostingId}")]
        public async Task<IActionResult> DeleteBookmark(int jobPostingId)
        {
            await _mediator.Send(new DeleteBookmarkJobCommand(jobPostingId));
            return NoContent();
        }


        [HttpGet("bookmarked-jobs")]
       // [Cached(600)]
        public async Task<IActionResult> GetBookmarkedJobs()
        {
            var bookmarkedJobs = await _mediator.Send(new GetBookmarkedJobsQuery());
            return Ok(bookmarkedJobs);
        }
    }
}
