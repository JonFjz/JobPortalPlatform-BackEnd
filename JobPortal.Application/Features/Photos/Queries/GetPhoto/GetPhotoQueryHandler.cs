using System;
using AutoMapper;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Features.Photos.Dtos;
using JobPortal.Domain.Entities;
using MediatR;

namespace JobPortal.Application.Features.Photos.Queries.GetPhoto
{
	public class GetPhotoQueryHandler : IRequestHandler<GetPhotoQuery, PhotoDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsPrincipalAccessor _claimsAccessor;
        private readonly IMapper _mapper;

        public GetPhotoQueryHandler(IUnitOfWork unitOfWork, IClaimsPrincipalAccessor claimsAccessor, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _claimsAccessor = claimsAccessor;
            _mapper = mapper;
        }

        public async Task<PhotoDto> Handle(GetPhotoQuery request, CancellationToken cancellationToken)
        {
            var employeer = await _claimsAccessor.GetCurrentEmployerAsync();

            var photo = (await _unitOfWork.Repository<Photo>().GetByConditionAsync(r => r.EmployerId == employeer.Id)).FirstOrDefault();

            if (photo == null)
            {
                return null;
            }

            var photoDto = _mapper.Map<PhotoDto>(photo);
            return photoDto;
        }

    }
}
