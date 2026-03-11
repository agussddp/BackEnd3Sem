using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TipoEventoController : ControllerBase
{
    private ITipoEventoRepository _tipoEventorepository;

    //Injecao de dependencia
    public TipoEventoController(ITipoEventoRepository tipoEventorepository)
    {
        _tipoEventorepository = tipoEventorepository;
    }

    /// <summary>
    ///  Endpoint da api que faz a chamada para o método de lista os tipos de evento
    /// </summary>
    /// <returns> status code 200 e lista os tipos de evento</returns>
    [HttpGet]
    public IActionResult Listar()
    {
        try 
        {
            return Ok(_tipoEventorepository.Listar());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    /// <summary>
    /// Endpoint da API que faz a chamada para o método de buscar   um tipo de evento específico
    /// </summary>
    /// <param name="id">id do tipo de evento buscado</param>
    /// <returns>Status code 200 e o tipo de evento buscado</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_tipoEventorepository.BuscarPorId(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    /// <summary>
    /// Endpoint da api que faz a chamada para o método de cadastrar um tipo de evento
    /// </summary>
    /// <param name="tipoEvento">Tipo de evento a ser cadastrado</param>
    /// <returns>Status code 201 e o tipo de evento a ser cadastrado</returns>
    [HttpPost]
    public IActionResult Cadastrar(TipoEventoDTO tipoEvento) 
    {
        try
        {
            var novoTipoEvento = new TipoEvento
            {
                Titulo = tipoEvento.Titulo!
            };
            _tipoEventorepository.Cadastrar(novoTipoEvento);

            return StatusCode(201, novoTipoEvento);
        }
        catch (Exception e) 
        {
            return BadRequest(e.Message);
        }
    }


    /// <summary>
    ///  Endpoint da API que faz a chamada para o método de atualizar um tipo de evento
    /// </summary>
    /// <param name="id"> ID Do tipo de evevento a ser atualizado</param>
    /// <param name="tipoEvento"> tipo de evento com os dados</param>
    /// <returns>Status Code 204 e o tipo de evento atualizados</returns>
    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, TipoEventoDTO tipoEvento) 
    {
        try
        {
            var tipoEventoAtualizado = new TipoEvento
            {
                Titulo = tipoEvento.Titulo!
            };
            _tipoEventorepository.Atualizar(id, tipoEventoAtualizado);

            return StatusCode(204, tipoEvento);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message); 
        }
    }

    /// <summary>
    ///  Endpoint da APi que faz a chamada para o método de deletar um tipo de evento
    /// </summary>
    /// <param name="id">Id tipo de evento a ser excluido</param>
    /// <returns>Status code 204</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id) 
    {
        try
        {
            _tipoEventorepository.Deletar(id);

            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message); 
        }
    }
}
