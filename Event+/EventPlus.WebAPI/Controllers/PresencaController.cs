using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.DTOs;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PresencaController : ControllerBase
{
    private readonly IPresencaRepository _presencaRepository;

    public PresencaController(IPresencaRepository presencaRepository)
    {
        _presencaRepository = presencaRepository;
    }

    /// <summary>
    /// EndPoint da API que retorna uma lista de presencas de um usuario especifico
    /// </summary>
    /// <param name="idUsuario">id usuario para filtragem</param>
    /// <returns>Estatus code 200 e uma lista de presenca</returns>
    [HttpGet("ListarMinhas/{idUsuario}")]
    public IActionResult BuscarMinhas(Guid idUsuario)
    {
        try
        {
            return Ok(_presencaRepository.ListarMinhas(idUsuario));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_presencaRepository.Listar());
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
            var presenca = _presencaRepository.BuscarPorId(id);
            if (presenca == null)
                return NotFound();
            return Ok(presenca);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
    /// <summary>
    /// feito
    /// </summary>
    /// <param name="presenca"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Inscrever(PresencaDTO presenca)
    {
        try
        {
            var novaPresenca = new Presenca
            {
                Situacao = presenca.Situacao,
                IdUsuario = presenca.IdUsuario,
                IdEvento = presenca.IdEvento
            };
            _presencaRepository.Inscrever(novaPresenca);
            return StatusCode(201, novaPresenca);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    [HttpPut]
    public IActionResult Atualizar(Guid id, Presenca presenca)
    {
        try
        {
            _presencaRepository.Atualizar(id, presenca);
            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro);
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
            _presencaRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
}