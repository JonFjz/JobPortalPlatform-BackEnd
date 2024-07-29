using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Features.Photos.Queries.GetPhoto;
using JobPortal.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

public class GetPhotoQueryHandler : IRequestHandler<GetPhotoQuery, FileStreamResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClaimsPrincipalAccessor _claimsAccessor;
    private readonly IBlobStorageService _blobService;

    public GetPhotoQueryHandler(IUnitOfWork unitOfWork, IClaimsPrincipalAccessor claimsAccessor, IBlobStorageService blobService)
    {
        _unitOfWork = unitOfWork;
        _claimsAccessor = claimsAccessor;
        _blobService = blobService;
    }

    public async Task<FileStreamResult> Handle(GetPhotoQuery request, CancellationToken cancellationToken)
    {
        var employer = await _claimsAccessor.GetCurrentEmployerAsync();
        var photo = (await _unitOfWork.Repository<Photo>().GetByConditionAsync(p => p.EmployerId == employer.Id)).FirstOrDefault();

        if (photo == null)
        {
            return null;
        }

        var fileStream = await _blobService.DownloadLogoAsync(photo.BlobUniqueName);

        return new FileStreamResult(new MemoryStream(fileStream), "image/jpeg")
        {
            FileDownloadName = "photo.jpg"
        };
    }
}
