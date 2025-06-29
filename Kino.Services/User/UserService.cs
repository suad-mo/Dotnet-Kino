using AutoMapper;
using Kino.Core.Authentication;
using Kino.Core.Entities;
using Kino.Core.ViewModels;
using Kino.Services.Mail;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Kino.Services.User
{
    public class UserService(UserManager<UserAccount> userManager, IConfiguration configuration, IMapper mapper, IMailService mailService) : IUserService
    {
        private readonly UserManager<UserAccount> _userManager = userManager;
        private readonly IConfiguration _configuration = configuration;
        private readonly IMapper _mapper = mapper;
        private readonly IMailService _mailService = mailService;

        public async Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel registerViewModel)
        {
            if (registerViewModel == null)
                throw new NullReferenceException("Model ne postoji");

            if (registerViewModel.Password != registerViewModel.PasswordConfirmation)
            {
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    ErrorMessage = "Lozinke se ne podudaraju."
                };
            }

            var existingUser = await _userManager.FindByEmailAsync(registerViewModel.Email);
            if (existingUser != null)
            {
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    ErrorMessage = "Korisnik sa email adresom već postoji."
                };
            }

            var identityUser = new UserAccount
            {
                Ime = registerViewModel.Ime,
                Prezime = registerViewModel.Prezime,
                Email = registerViewModel.Email,
                UserName = registerViewModel.Email,
                LockoutEnabled = false
            };

            if (!registerViewModel.Email.Equals("dzejlanar@gmail.com"))
                identityUser.EmailConfirmed = true;

            var result = await _userManager.CreateAsync(identityUser, registerViewModel.Password);

            if (result.Succeeded)
            {
                var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);
                var encodedEmailToken = Encoding.UTF8.GetBytes(emailConfirmationToken);
                var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

                string url = $"{_configuration["WebUrl"]}confirm-email?userId={identityUser.Id}&token={validEmailToken}";

                await _mailService.SendEmailAsync(identityUser.Email, "Potvrda email adrese",
                    $"<h1>Dobrodošli na Multiplex Kino!</h1><p>Potvrdite vašu email adresu klikom na link: {url}</p>");

                return new UserManagerResponse
                {
                    Message = "Korisnik uspješno kreiran",
                    IsSuccess = true
                };
            }

            return new UserManagerResponse
            {
                IsSuccess = false,
                ErrorMessage = "Greška prilikom kreiranja korisnika"
            };
        }

        public async Task<UserManagerResponse> LoginUserAsync(LoginViewModel loginViewModel)
        {
            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);

            if (user == null)
                return new UserManagerResponse
                {
                    ErrorMessage = "Nepostojeći korisnik!",
                    IsSuccess = false
                };

            if (user.LockoutEnabled)
                return new UserManagerResponse
                {
                    ErrorMessage = "User je obrisan!",
                    IsSuccess = false
                };

            if (!await _userManager.IsEmailConfirmedAsync(user))
                return new UserManagerResponse
                {
                    ErrorMessage = "Email Adresa nije potvrđena.",
                    IsSuccess = false
                };

            var result = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);

            if (!result)
                return new UserManagerResponse
                {
                    ErrorMessage = "Netačni podaci!",
                    IsSuccess = false
                };

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, loginViewModel.Email)
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var jwtKey = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(jwtKey))
                throw new InvalidOperationException("JWT key is not configured.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserManagerResponse
            {
                JwtToken = tokenAsString,
                Message = "Korisnik uspješno prijavljen.",
                IsSuccess = true,
                ExpireDate = token.ValidTo
            };
        }

        public async Task<List<UpdateUserViewModel>> GetAllUsers()
        {
            return await Task.Run(() =>
            {
                return _mapper.Map<IQueryable<UserAccount>, List<UpdateUserViewModel>>(_userManager.Users);
            });
        }

        public async Task<IdentityResult> UpdateUser(UpdateUserViewModel user)
        {
            var foundUser = await _userManager.FindByIdAsync(user.Id);
            IdentityResult result = IdentityResult.Failed();
            if (foundUser != null)
            {
                foundUser.Email = user.Email;
                foundUser.UserName = user.Email;
                foundUser.Ime = user.Ime;
                foundUser.Prezime = user.Prezime;
                result = await _userManager.UpdateAsync(foundUser);
            }
            else result = IdentityResult.Failed(new IdentityError() { Code = "IdNotFound", Description = "Korisnik ne postoji." });

            return result;
        }

        public async Task<IdentityResult> DeleteUser(string email)
        {
            var foundUser = await _userManager.FindByEmailAsync(email);
            if (foundUser == null) return IdentityResult.Failed(new IdentityError() { Code = "EmailNotFound", Description = $"Korisnik sa emailom {email} ne postoji." });

            foundUser.LockoutEnabled = true;

            return await _userManager.UpdateAsync(foundUser);
        }

        public async Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "Korisnik nije pronađen"
                };

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ConfirmEmailAsync(user, normalToken);

            if (result.Succeeded)
                return new UserManagerResponse
                {
                    IsSuccess = true,
                    Message = "Uspješno potvrđena email adresa!"
                };

            return new UserManagerResponse
            {
                IsSuccess = false,
                ErrorMessage = "Email adresa nije potvrđena."
            };
        }

        public async Task<UpdateUserViewModel> GetUser(string id)
        {
            var userAccount = await _userManager.FindByIdAsync(id);
            if (userAccount == null)
            {
                ArgumentNullException argumentNullException = new(nameof(id), "UserAccount not found.");
                throw argumentNullException;
            }
            return _mapper.Map<UserAccount, UpdateUserViewModel>(userAccount);
        }

        public Task<UserManagerResponse> AddUser(UpdateUserViewModel user)
        {
            string password = "Qwerty123456!";
            var registerViewModel = new RegisterViewModel()
            {
                Ime = user.Ime,
                Prezime = user.Prezime,
                Email = user.Email,
                Password = password,
                PasswordConfirmation = password
            };

            return this.RegisterUserAsync(registerViewModel);
        }
    }
}
