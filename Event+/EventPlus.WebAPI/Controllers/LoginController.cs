using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Repositories;
using EventPlus.WebAPI.Repositorios;
using EventPlus.WebAPI.BdContextEvent;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace FilmesMoura1.WebAPI.Controllers;

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
    public IActionResult Login(LoginDTO loginDto)
    {
        try
        {
            Usuario usuarioBuscado = _usuarioRepository.BuscarPorEmailESenha(loginDto.Email!, loginDto.Senha!, loginDto.Titulo!);
            if (usuarioBuscado == null)
            {
                return NotFound("Email ou Senha Inválidos");
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario.ToString()),
                new Claim(JwtRegisteredClaimNames.Email,usuarioBuscado.Email!)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("Filmes-Chave-Autenticacao-webapi-dev"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "api_events",
                audience: "api_events",
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: creds
                );
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}