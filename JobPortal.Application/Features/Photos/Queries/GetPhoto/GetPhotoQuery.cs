using System;
using JobPortal.Application.Features.Photos.Dtos;
using JobPortal.Application.Features.Resumes.Dtos;
using MediatR;

namespace JobPortal.Application.Features.Photos.Queries.GetPhoto
{
	public class GetPhotoQuery : IRequest<PhotoDto>
    { 
	}
}

