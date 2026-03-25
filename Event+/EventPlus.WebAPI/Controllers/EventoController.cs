using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventoController : ControllerBase
{
    private IEventoRepository _eventoRepository;
    public EventoController(IEventoRepository eventoRepository)
    {
        _eventoRepository = eventoRepository;
    }
    /// <summary>
    /// EndPoint da API que faz a chamada para o metodo de listar eventos filtrado pelo usuario
    /// </summary>
    /// <param name="idUsuario">Id do usuario filtragem</param>
    /// <returns>Lista de eventos filtrados por usuario</returns>
    [HttpGet("Usuario/{idUsuario}")]
    public IActionResult ListarPorId(Guid idUsuario)
    {
        try
        {
            return Ok(_eventoRepository.ListarPorId(idUsuario));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// EndPoint da API que faz a chamada para o metodo de listar os proximos eventos
    /// </summary>
    /// <returns>Status code 200 e uma lista de proximos eventos </returns>
    [HttpGet("ListarProximos")]
    public IActionResult BuscarProximos()
    {
        try
        {
            return Ok(_eventoRepository.ListarProximos());
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_eventoRepository.Listar());
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_eventoRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="evento"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Cadastrar(EventoDTO evento)
    {
        try
        {
            var novoevento = new Evento
            {
                Nome = evento.Nome!,
                Descricao = evento.Descricao!,
                DataEvento = evento.DataEvento!,
                IdTipoEvento = evento.IdTipoEvento!,
                IdInstituicao = evento.IdInstituicao!
            };
            _eventoRepository.Cadastrar(novoevento);
            return StatusCode(201);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _eventoRepository.Deletar(id);
            return StatusCode(204);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="evento"></param>
    /// <returns></returns>
    [HttpPut]
    public IActionResult Atualizar(Guid id, EventoDTO evento)
    {
        var novoevento = new Evento
        {
            Nome = evento.Nome!,
            Descricao = evento.Descricao!,
            DataEvento = evento.DataEvento!,
            IdTipoEvento = evento.IdTipoEvento!,
            IdInstituicao = evento.IdInstituicao!
        };
        try
        {
            _eventoRepository.Atualizar(id, novoevento);
            return StatusCode(204);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

}