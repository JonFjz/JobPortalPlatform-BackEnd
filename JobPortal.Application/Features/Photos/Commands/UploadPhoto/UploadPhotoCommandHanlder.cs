using System;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Domain.Entities;
using MediatR;

namespace JobPortal.Application.Features.Photos.Commands.UploadPhoto
{
	public class UploadPhotoCommandHanlder : IRequestHandler<UploadPhotoCommand, int>
    {
        private readonly IBlobStorageService _blobService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsPrincipalAccessor _claimsAccessor;

        public UploadPhotoCommandHanlder(IBlobStorageService blobService, IUnitOfWork unitOfWork, IClaimsPrincipalAccessor claimsAccessor)
        {
            _blobService = blobService;
            _unitOfWork = unitOfWork;
            _claimsAccessor = claimsAccessor;
        }

        public async Task<int> Handle(UploadPhotoCommand request, CancellationToken cancellationToken)
        {
            var employer = await _claimsAccessor.GetCurrentEmployerAsync();

            var url = await _blobService.UploadFileAsync(request.File.OpenReadStream(), request.File.FileName);

            var photo = new Photo
            {
                Url = url,
                UploadedAt = DateTime.UtcNow,
                EmployerId = employer.Id
            };

            await _unitOfWork.Repository<Photo>().CreateAsync(photo);
            _unitOfWork.Complete();

            return photo.Id;
        }
    }
}

