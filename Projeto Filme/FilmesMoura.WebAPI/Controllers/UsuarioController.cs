using FilmesMoura.WebAPI.Models;

using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interface;


namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;
    public UsuarioController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    [HttpPost]
    public IActionResult Post(Usuario novoUsuario)
    {
        try
        {
            _usuarioRepository.Cadastrar(novoUsuario);
            return StatusCode(201, novoUsuario);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);

        }



    }
}


