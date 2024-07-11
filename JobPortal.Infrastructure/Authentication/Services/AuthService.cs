using AutoMapper;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Features.Users.Dtos;
using JobPortal.Application.Responses;
using JobPortal.Domain.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Infrastructure.Authentication.Services
{
    public class AuthService : IAuthService
    { 
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager,  ITokenService tokenService, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<AuthResponse> Register(RegisterDto registerDto)
        {
            var user = _mapper.Map<User>(registerDto);

            user.UserName = registerDto.Username.ToLower();

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            var roleResult = await _userManager.AddToRoleAsync(user, "JobSeeker");

            return new AuthResponse
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
            };
        }


        public async Task<AuthResponse> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users
            .SingleOrDefaultAsync(x => x.Email == loginDto.Email.ToLower());


            var result = await _signInManager
                .CheckPasswordSignInAsync(user, loginDto.Password, false);

            return new AuthResponse
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
            };
        }


    }
}
