

using FilmesMoura.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebApplication1.DTO;
using WebApplication1.Interface;


namespace Filmes.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;
    public LoginController(IUsuarioRepository usuarioRepository)
    {

        _usuarioRepository = usuarioRepository;

    }

    [HttpPost]
    public IActionResult Login(LoginDTO loginDTO)
    {
        try
        {
            Usuario usuarioBuscado = _usuarioRepository.BuscarPorEmailESenha(loginDTO.Email!, loginDTO.Senha!);
            if (usuarioBuscado == null)
            {
                return NotFound("Email ou Senha inválidos");
            }
            var claims = new[]
            {
                  new Claim(JwtRegisteredClaimNames.Jti,usuarioBuscado.IdUsuario),

                  new Claim(JwtRegisteredClaimNames.Email,usuarioBuscado.Email),

            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("filmes-chave-autenticacao-webapi-dev"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (

                issuer: "api_filmes",

                audience: "api_filmes",

                claims: claims,

                expires: DateTime.Now.AddDays(5),

                signingCredentials: creds

             );

            return Ok(new
            {

                token = new JwtSecurityTokenHandler().WriteToken(token)
            });


        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }


    }
}
