using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.Application.Features.Photos.Queries.GetPhoto
{
    public class GetPhotoQuery : IRequest<FileStreamResult>
    {
    }
}

