using System;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Features.Photos.Dtos;
using JobPortal.Application.Features.Resumes.Dtos;
using JobPortal.Application.Features.Resumes.Queries.DownloadResume;
using JobPortal.Domain.Entities;
using MediatR;

namespace JobPortal.Application.Features.Photos.Queries.DownloadPhoto
{
    public class DownloadPhotoQueryHandler : IRequestHandler<DownloadPhotoQuery, PhotoResult>
    {
        private readonly IBlobStorageService _blobService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsPrincipalAccessor _claimsAccessor;

        public DownloadPhotoQueryHandler(IBlobStorageService blobService, IUnitOfWork unitOfWork, IClaimsPrincipalAccessor claimsAccessor)
        {
            _blobService = blobService;
            _unitOfWork = unitOfWork;
            _claimsAccessor = claimsAccessor;
        }

        public async Task<PhotoResult> Handle(DownloadPhotoQuery request, CancellationToken cancellationToken)
        {
            var employer = await _claimsAccessor.GetCurrentEmployerAsync();
            var photo = (await _unitOfWork.Repository<Photo>().GetByConditionAsync(r => r.EmployerId == employer.Id)).FirstOrDefault();

            if (photo == null)
            {
                throw new KeyNotFoundException("Photo not found.");
            }

            var fileData = await _blobService.DownloadFileAsync(photo.Url);

            return new PhotoResult
            {
                FileData = fileData,
                FileName = photo.Url ?? "photo.jpg"
            };
        }
    }
}

