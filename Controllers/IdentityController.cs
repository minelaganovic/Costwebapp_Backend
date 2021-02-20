using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CostApp.Contract;
using CostApp.Data;
using CostApp.Data.Identity;
using CostApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CostApp.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class IdentityController : Controller
    {
        private readonly IIdentityService _identityService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public IdentityController(IIdentityService identityService, UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _identityService = identityService;
            _userManager = userManager;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register([FromBody] CreateUserRequest request)
        {
            var authResponse = await _identityService.RegisterAsync(request.Email, request.Password, request.FirstName, request.LastName);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            var sendedEmail = sendConfimationEmail(authResponse.User);

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken,
                Admin = authResponse.Admin,
                FirstName = authResponse.User.FirstName,
                LastName = authResponse.User.LastName,
                Note = authResponse.User.Note
            }) ;
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request, string returnUrl = null)
        {
            var authResponse = await _identityService.LoginAsync(request.Email, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken,
                Admin = authResponse.Admin,
                FirstName = authResponse.User.FirstName,
                LastName = authResponse.User.LastName,
                Note = authResponse.User.Note
            });
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.Identity.Refresh)]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var authResponse = await _identityService.RefreshTokenAsync(request.Token, request.RefreshToken);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken,
                Admin = authResponse.Admin,
                FirstName = authResponse.User.FirstName,
                LastName = authResponse.User.LastName,
                Note = authResponse.User.Note
            });
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Identity.ConfirmEmail)]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var confirmedEmail = await _identityService.ConfirmEmailAsync(token, email);

            if (!confirmedEmail)
                return BadRequest();
			return Redirect("http://localhost:8080/signin");
        }

        [HttpPut(ApiRoutes.Identity.Update)]
        public async Task<IActionResult> Update([FromBody] UserUpdateRequest request)
        {
            var userEmail = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var authResponse = await _identityService.UpdateUserAsync(userEmail, request.FirstName, request.LastName, request.Note);

            if (!authResponse)
            {
                return BadRequest("Some of the parameters are wrong.");
            }

            return Ok();
        }

        private async Task sendConfimationEmail(AppUser appUser)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Identity", new { token, email = appUser.Email }, Request.Scheme);

            var message = new Message(new string[] { appUser.Email }, "Confirmation email link", confirmationLink);
            EmailConfiguration emailConfig = new EmailConfiguration()
            {
                From = _configuration.GetValue<string>("EmailConfiguration:From"),
                UserName = _configuration.GetValue<string>("EmailConfiguration:Username"),
                Password = _configuration.GetValue<string>("EmailConfiguration:Password"),
                Port = _configuration.GetValue<int>("EmailConfiguration:Port"),
                SmtpServer = _configuration.GetValue<string>("EmailConfiguration:SmtpServer")
            };
            IEmailSender emailSender = new EmailSender(emailConfig);
            emailSender.SendEmail(message);
        }
    }
}