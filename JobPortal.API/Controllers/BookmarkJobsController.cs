using JobPortal.Application.Features.BookmarkJobs.Commands.CreateBookmarkJob;
using JobPortal.Application.Features.BookmarkJobs.Commands.RemoveBookmarkJob;
using JobPortal.Application.Features.BookmarkJobs.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers
{
    [ApiController]
    [Authorize]
    public class BookmarkJobsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BookmarkJobsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Policy = "JobSeeker")]
        [HttpPost("bookmarks")]
        public async Task<IActionResult> AddBookmark([FromBody] CreateBookmarkJobCommand command)
        { 
            var result = await _mediator.Send(command);
            return Ok(new { JobPostingId = result });
        }

        [Authorize(Policy = "JobSeeker")]
        [HttpDelete("bookmarks/{jobPostingId}")]
        public async Task<IActionResult> RemoveBookmark(int jobPostingId)
        {
            await _mediator.Send(new RemoveBookmarkJobCommand(jobPostingId));
            return NoContent();
        }

        [Authorize(Policy = "JobSeeker")]
        [HttpGet("bookmarked-jobs")]
        public async Task<IActionResult> GetBookmarkedJobs()
        {
            var bookmarkedJobs = await _mediator.Send(new GetBookmarkedJobsQuery());
            return Ok(bookmarkedJobs);
        }
    }
}
