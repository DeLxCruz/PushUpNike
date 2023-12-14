// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;

// namespace API.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class AuthController : ControllerBase
//     {
//         [AllowAnonymous]
//         [HttpPost("login")]
//         public IActionResult Login(string username, string password)
//         {
//             // Lógica de autenticación aquí...

//             var user = _userService.Authenticate(username, password);

//             if (user == null)
//                 return Unauthorized();

//             var token = GenerateJwtToken(user);
//             var refreshToken = GenerateRefreshToken(user.Id);

//             return Ok(new { token, refreshToken });
//         }

//         private string GenerateJwtToken(Usuario user)
//         {
//             var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]));
//             var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//             var claims = new List<Claim>
//     {
//         new Claim(ClaimTypes.Name, user.NombreUsuario),
//         new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
//         // Puedes agregar más claims según la información del usuario o roles
//     };

//             var token = new JwtSecurityToken(
//                 _configuration["Jwt:Issuer"],
//                 _configuration["Jwt:Issuer"],
//                 claims,
//                 expires: DateTime.Now.AddHours(1),
//                 signingCredentials: credentials
//             );

//             return new JwtSecurityTokenHandler().WriteToken(token);
//         }

//         private string GenerateRefreshToken(int userId)
//         {
//             var refreshToken = new RefreshToken
//             {
//                 UserId = userId,
//                 Token = Guid.NewGuid().ToString(),
//                 ExpiryDate = DateTime.Now.AddMonths(1) // Puedes ajustar el tiempo de expiración
//             };

//             _userRepository.AddRefreshToken(refreshToken);
//             _unitOfWork.SaveChanges();

//             return refreshToken.Token;
//         }
//     }
// }