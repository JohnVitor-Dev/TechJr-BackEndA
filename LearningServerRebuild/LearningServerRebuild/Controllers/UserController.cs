using LearningServerRebuild.Helper;
using LearningServerRebuild.Models;
using LearningServerRebuild.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using System.Security.Claims;

namespace UserC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ProjectContext _context;
        private readonly IConfiguration _configuration;
        private readonly IEmail _email;

        public UserController(ProjectContext context, IConfiguration configuration, IEmail email)
        {
            _context = context;
            _configuration = configuration;
            _email = email;
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> CreateUser(Users user)
        {
            if (_context.User.Any(u => u.email == user.email))
            {
                var errorMessage = "Email is already registered.";
                var errorResponse = new { message = errorMessage };
                return Conflict(errorResponse);
            }

            user.createdAt = DateTime.UtcNow;
            user.updatedAt = DateTime.UtcNow;

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            var successResponse = new
            {
                Email = user.email,
                Name = user.name
            };

            return CreatedAtAction(nameof(GetUser), successResponse);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(Login login)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.email == login.email);

            if(user == null)
            {
                var errorMessage = "Email not registered";
                var errorResponse = new { message = errorMessage };
                return NotFound(errorResponse);

            } else if (user.password != login.password)
            {
                var errorMessage = "Incorrect password";
                var errorResponse = new { message = errorMessage };
                return NotFound(errorResponse);

            } else if (user != null && user.password == login.password)
            {
                var token = TokenService.GenerateToken(user, _configuration);
                return Ok(new { token });
            }

            return NotFound();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUser()
        {
            var userIDClaim = User.FindFirstValue("UserID");

            if (!Guid.TryParse(userIDClaim, out Guid userID))
            {
                var errorMessage = "Invalid user ID claim";
                var errorResponse = new { message = errorMessage };
                return BadRequest(errorResponse);
            }

            var user = await _context.User.FirstOrDefaultAsync(u => u.id == userID);

            if (user == null)
            {
                var errorMessage = "User not found";
                var errorResponse = new { message = errorMessage };
                return NotFound(errorResponse);
            }

            var userResponse = new
            {
                id = user.id,
                email = user.email,
                name = user.name,
                createdAt = user.createdAt,
                updatedAt = user.updatedAt
            };

            return Ok(userResponse);

        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteUser()
        {
            var userIDClaim = User.FindFirstValue("UserID");

            if (!Guid.TryParse(userIDClaim, out Guid userID))
            {
                var errorMessage = "Invalid user ID claim";
                var errorResponse = new { message = errorMessage };
                return BadRequest(errorResponse);
            }

            var user = await _context.User.FirstOrDefaultAsync(u => u.id == userID);

            if (user == null)
            {
                var errorMessage = "User not found";
                var errorResponse = new { message = errorMessage };
                return NotFound(errorResponse);
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return Ok();

        }

        [HttpPatch]
        [Authorize]
        public async Task<IActionResult> UpdateUser(Users userUpdate)
        {
            var userIDClaim = User.FindFirstValue("UserID");

            if (!Guid.TryParse(userIDClaim, out Guid userID))
            {
                var errorMessage = "Invalid user ID claim";
                var errorResponse = new { message = errorMessage };
                return BadRequest(errorResponse);
            }

            var user = await _context.User.FirstOrDefaultAsync(u => u.id == userID);

            if (user == null)
            {
                var errorMessage = "User not found";
                var errorResponse = new { message = errorMessage };
                return NotFound(errorResponse);
            }

            if (_context.User.Any(u => u.email == userUpdate.email && u.id != user.id))
            {
                var errorMessage = "Email is already registered.";
                var errorResponse = new { message = errorMessage };
                return Conflict(errorResponse);
            }

            if (user.email == userUpdate.email)
            {
                var errorMessage = "The email entered is the same as the previous email";
                var errorResponse = new { message = errorMessage };
                return Conflict(errorResponse);
            }
            else if (user.name == userUpdate.name)
            {
                var errorMessage = "The name entered is the same as the previous name";
                var errorResponse = new { message = errorMessage };
                return Conflict(errorResponse);
            }
            else if (user.password == userUpdate.password)
            {
                var errorMessage = "The password entered is the same as the previous password";
                var errorResponse = new { message = errorMessage };
                return Conflict(errorResponse);
            }

            user.email = userUpdate.email;
            user.password = userUpdate.password;
            user.name = userUpdate.name;
            user.updatedAt = DateTime.UtcNow;

            _context.User.Update(user);
            await _context.SaveChangesAsync();

            var userResponse = new { user.id, user.email, user.name, user.createdAt, user.updatedAt };

            return Ok(userResponse);


        }

        [HttpPost("forgot")]
        public async Task<IActionResult> SendEmailForgotPassword(EmailToReset emailToReset)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.email == emailToReset.email);

            if (user == null)
            {
                var errorMessage = "Email not registered";
                var errorResponse = new { message = errorMessage };
                return NotFound(errorResponse);
            }

            var token = TokenService.GenerateToken(user, _configuration);
            string mensagem = $"Seu token de redefinição de senha é: {token}";

            bool mail = _email.Enviar(emailToReset.email, "Sistema de Email(JYS) - Token de redefinição", mensagem);

            if (mail)
            {
                var message = new { message = "O token foi enviado para seu email, verifique sua caixa de entrada ou spam." };
                return Ok(message);
            }
            else
            {
                var message = new { message = "Ocorreu um erro ao enviar o email, tente novamente." };
                return BadRequest(message);
            }
        }

        [HttpPost("reset")]
        [Authorize]
        public async Task<IActionResult> ResetPassword(PasswordReset passwordReset)
        {
            var userIDClaim = User.FindFirstValue("UserID");

            if (!Guid.TryParse(userIDClaim, out Guid userID))
            {
                var errorMessage = "Invalid user ID claim";
                var errorResponse = new { message = errorMessage };
                return BadRequest(errorResponse);
            }

            var user = await _context.User.FirstOrDefaultAsync(u => u.id == userID);

            if (user == null)
            {
                var errorMessage = "User not found";
                var errorResponse = new { message = errorMessage };
                return NotFound(errorResponse);
            }

            user.password = passwordReset.password;
            user.updatedAt = DateTime.UtcNow;

            _context.User.Update(user);
            await _context.SaveChangesAsync();

            var userResponse = new
            {
                email = user.email,
                name = user.name
            };

            return Ok(userResponse);

        }

        

    }
}
