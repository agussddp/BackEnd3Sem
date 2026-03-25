using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Identity.Client;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TipoEventoController : ControllerBase
{
    private ITipoEventoRepository _tipoEventoRepository;
    //injeção de dependencia
    public TipoEventoController(ITipoEventoRepository tipoEventoRepository)
    {
        _tipoEventoRepository = tipoEventoRepository;
    }
    /// <summary>
    /// Endpoint da api que faz a chamada para o metodo de lista os tipos de evento
    /// </summary>
    /// <returns>Status code 200 e alista os tipos de evento</returns>
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_tipoEventoRepository.Listar());
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da api que faz a chamada para o metodo de buscar um tipo de evento especifico
    /// </summary>
    /// <param name="id">id do tipo de evento buscado</param>
    /// <returns>Status code 200 e o tipo de evento buscado</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_tipoEventoRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API faz a chamada para o metodo de cadastro um tipo de evento 
    /// </summary>
    /// <param name="tipoEvento">Tipo de evento a ser cadastrado</param>
    /// <returns>Status Code 201 e o tipo de evento a ser cadastrado</returns>
    [HttpPost]
    public IActionResult Cadastrar(TipoEventoDTO tipoEvento)
    {
        try
        {
            var novoTipoEvento = new TipoEvento
            {
                Titulo = tipoEvento.Titulo!
            };
            _tipoEventoRepository.Cadastrar(novoTipoEvento);
            return StatusCode(201, novoTipoEvento);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
    /// <summary>
    /// Endpoint da API que faz a chamada para o metodo de atualizar um tipo de evento
    /// </summary>
    /// <param name="id">Id do tipo evento a ser atualizado</param>
    /// <param name="tipoEvento">tipo de evento com os dados atualizados</param>
    /// <returns>Status Code 204 e o tipo de evento atualizado</returns>
    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, TipoEventoDTO tipoEvento)
    {
        try
        {
            var novoTipoEvento = new TipoEvento
            {
                Titulo = tipoEvento.Titulo!
            };
            _tipoEventoRepository.Atualizar(id, novoTipoEvento);
            return StatusCode(204, tipoEvento);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
    /// <summary>
    /// Endpoint da API que faz a chamada para o metodo de deletar um tipo de evento
    /// </summary>
    /// <param name="id">Id do tipo do evento a ser excluido</param>
    /// <returns>Status Code 204</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        try
        {
            _tipoEventoRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }


}