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

            var existingPhoto = await _unitOfWork.Repository<Photo>().GetByConditionAsync(p => p.EmployerId == employer.Id);

            if (existingPhoto != null && existingPhoto.Any())
            {
                await _blobService.DeletePhotoAsync(existingPhoto.First().BlobUniqueName);

                _unitOfWork.Repository<Photo>().Delete(existingPhoto.First());
            }

            var uniqueBlobName = await _blobService.UploadLogoAsync(request.File.OpenReadStream(), request.File.FileName);

            var photo = new Photo
            {
                OriginalPhotoName = request.File.FileName,
                BlobUniqueName = uniqueBlobName,
                UploadedAt = DateTime.UtcNow,
                EmployerId = employer.Id
            };

            await _unitOfWork.Repository<Photo>().CreateAsync(photo);
            _unitOfWork.Complete();

            return photo.Id;
        }
    }
}

