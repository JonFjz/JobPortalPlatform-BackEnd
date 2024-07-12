﻿using System.Security.Claims;

namespace JobPortal.Application.Contracts.Infrastructure
{
    public interface IClaimsPrincipalAccessor
    {
        ClaimsPrincipal ClaimsPrincipal { get; }
    }
}
