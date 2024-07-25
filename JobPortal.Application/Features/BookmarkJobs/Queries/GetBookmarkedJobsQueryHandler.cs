using AutoMapper;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Features.BookmarkJobs.Dtos;
using JobPortal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Application.Features.BookmarkJobs.Queries
{
    public class GetBookmarkedJobsQueryHandler : IRequestHandler<GetBookmarkedJobsQuery, List<BookmarkedJobDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;
        private readonly IMapper _mapper;

        public GetBookmarkedJobsQueryHandler(IUnitOfWork unitOfWork,IClaimsPrincipalAccessor claimsPrincipalAccessor,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
            _mapper = mapper;
        }
        public async Task<List<BookmarkedJobDto>> Handle(GetBookmarkedJobsQuery request, CancellationToken cancellationToken)
        {
            var jobSeeker = await _claimsPrincipalAccessor.GetCurrentJobSeekerAsync();
            var bookmarkedJobs =await _unitOfWork.Repository<BookmarkJob>().GetByCondition(x=>x.JobSeekerId == jobSeeker.Id).Include(x=>x.JobPosting).Include(x=>x.JobPosting.Employer).ToListAsync();
         

            var bookmarkedJobsDto=_mapper.Map<List<BookmarkedJobDto>>(bookmarkedJobs);
            
            return bookmarkedJobsDto;

        }
    }
}
