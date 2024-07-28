using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Domain.Entities;
using MediatR;

namespace JobPortal.Application.Features.Photos.Commands.DeletePhoto
{
    public class DeletePhotoCommandHandler : IRequestHandler<DeletePhotoCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsPrincipalAccessor _claimsAccessor;
        private readonly IBlobStorageService _blobService;

        public DeletePhotoCommandHandler(IUnitOfWork unitOfWork, IClaimsPrincipalAccessor claimsAccessor, IBlobStorageService blobService)
        {
            _unitOfWork = unitOfWork;
            _claimsAccessor = claimsAccessor;
            _blobService = blobService;
        }
	
        public async Task Handle(DeletePhotoCommand request, CancellationToken cancellationToken)
        {
     
            var employer = await _claimsAccessor.GetCurrentEmployerAsync();
            var photo = await _unitOfWork.Repository<Photo>().GetByConditionAsync(p => p.EmployerId == employer.Id);

            if (photo == null)
                throw new KeyNotFoundException("Photo not found.");
            

            await _blobService.DeletePhotoAsync(photo.First().BlobUniqueName);

            _unitOfWork.Repository<Photo>().Delete(photo.First());
            _unitOfWork.Complete();

        }

    }
}

