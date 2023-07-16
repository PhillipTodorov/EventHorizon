using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EventHorizonBackend.Models;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using static EventHorizonBackend.Controllers.UsersController;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace EventHorizonBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _config;

        public UsersController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        // Define a DTO for the registration model
        public class RegisterDto
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }
        }


        // Define a DTO for the login model
        public class LoginDto
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }
        }

        // POST: api/Users/register
        [HttpPost("register")]
        public async Task<ActionResult<IdentityUser>> Register(RegisterDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    ModelState.AddModelError(string.Empty, "Email already in use.");
                    return BadRequest(ModelState);
                }

                user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return user;
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return BadRequest(ModelState);
        }


        // POST: api/Users/login
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        var user = await _userManager.FindByEmailAsync(model.Email);

                        // Create a token handler
                        var tokenHandler = new JwtSecurityTokenHandler();

                        // Generate a Key
                        var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]); // Get the key from your appsettings.json

                        // Create a descriptor for the token
                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id) }),
                            Expires = DateTime.UtcNow.AddDays(7), // Token expiration, set it to your desired timeframe.
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                        };

                        // Create the token
                        var token = tokenHandler.CreateToken(tokenDescriptor);

                        // Write the token
                        var jwtToken = tokenHandler.WriteToken(token);

                        return Ok(new { token = jwtToken });
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    }
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return StatusCode(500, "Internal server error");
            }
        }
        // POST: api/Users/logout
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}
