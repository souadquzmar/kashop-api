using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace KASHOP.BLL.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        public AuthenticationService(UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return new LoginResponse() { Success = false, Message = "Invalid Email" };
            var isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
            if (!isEmailConfirmed)
                return new LoginResponse() { Success = false, Message = "Email is not confirmed" };
            var result = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!result)
                return new LoginResponse() { Success = false, Message = "Invalid Password" };
            return new LoginResponse() { Success = true, Message = "Logged in successfully" };
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            var user = request.Adapt<ApplicationUser>();
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                return new RegisterResponse() { Success = false, Message = "Error" };
            await _userManager.AddToRoleAsync(user, "User");
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            token = Uri.EscapeDataString(token);
            var emailUrl = $"http://localhost:5228/api/account/confirmemail?token={token}&userId={user.Id}";
            await _emailSender.SendEmailAsync(user.Email, "Welcome", $"<h1>Welcome {user.UserName}</h1>" + $"" + $"<a href='{emailUrl}'> Confirm </a>");
            return new RegisterResponse() { Success = true, Message = "Success" };
        }

        public async Task<bool> ConfirmEmailAsync(string token, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return false;
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
                return true;
            return false;
        }
    }
}