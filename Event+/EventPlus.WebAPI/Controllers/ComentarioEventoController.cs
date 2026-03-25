using Azure;
using Azure.AI.ContentSafety;
using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Xml.Schema;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ComentarioEventoController : ControllerBase
{
    private readonly ContentSafetyClient _contentSafetyClient;
    private readonly IComentarioEventoRepository _comentarioEventoRepository;
    public ComentarioEventoController(ContentSafetyClient contentSafetyClient, IComentarioEventoRepository comentarioEventoRepository)
    {
        _contentSafetyClient = contentSafetyClient;
        _comentarioEventoRepository = comentarioEventoRepository;
    }
    [HttpPost]
    public async Task<IActionResult> Post(ComentarioEventoDTO comentarioEvento)
    {
        try
        {
            if (string.IsNullOrEmpty(comentarioEvento.Descricao))
            {
                return BadRequest("A descrição do comentário é obrigatória.");
            }
            // criar objeto de analise 
            var request = new AnalyzeTextOptions(comentarioEvento.Descricao);

            // chamar a API do azure content safety
            Response<AnalyzeTextResult> response = await _contentSafetyClient.AnalyzeTextAsync(request);

            //verifica se o texto tem severidade maior q 0
            bool temConteudoImproprio = response.Value.CategoriesAnalysis.Any(c => c.Severity > 0);

            var novoComentario = new ComentarioEvento
            {
                IdEvento = comentarioEvento.IdEvento,
                IdUsuario = comentarioEvento.IdUsuario,
                Descricao = comentarioEvento.Descricao,
                ExibeComentario = !temConteudoImproprio,
                DataComentarioEvento = DateTime.Now
            };
            _comentarioEventoRepository.Cadastrar(novoComentario);
            return StatusCode(201, novoComentario);
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
            return Ok(_comentarioEventoRepository.Listar());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("BuscarPorIdDeUsuario")]
    public IActionResult BuscarPorIdDeUsuario(Guid idUsuario)
    {
        try
        {
            return Ok(_comentarioEventoRepository.BuscarPorIdUsuario(idUsuario));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("ListarSomenteExibe")]
    public IActionResult ListarSomenteExibe(Guid IdEvento)
    {
        try
        {
            return Ok(_comentarioEventoRepository.ListarSomenteExibe(IdEvento));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _comentarioEventoRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}