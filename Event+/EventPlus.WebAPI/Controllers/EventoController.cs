using EventPlus.WebAPI.DTOs;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class EventoController : ControllerBase
    {
        private readonly IEventoRepository _eventoRepository;

        public EventoController(IEventoRepository eventoRepository)
        {
            _eventoRepository = eventoRepository;
        }

        /// <summary>
        /// Lista todos os eventos cadastrados
        /// </summary>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return Ok(_eventoRepository.Listar());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Busca eventos filtrados pelas presenças confirmadas de um usuário
        /// </summary>
        /// <param name="idUsuario">ID do usuário para filtragem</param>
        [HttpGet("Usuario/{idUsuario}")]
        public IActionResult ListarPorId(Guid idUsuario)
        {
            try
            {
                return Ok(_eventoRepository.ListarPorId(idUsuario));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Lista os próximos eventos data maior ou igual à atual
        /// </summary>
        [HttpGet("ListarProximos")]
        public IActionResult BuscarProximos()
        {
            try
            {
                return Ok(_eventoRepository.ListarProximos());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Cadastra um novo evento 
        /// </summary>
        [HttpPost]
        public IActionResult Cadastrar(EventoDTO evento)
        {
            try
            {
                Evento novoEvento = new Evento
                {
                    Nome = evento.Nome!,
                    DataEvento = evento.DataEvento,
                    Descricao = evento.Descricao!,
                    
                };

                _eventoRepository.Cadastrar(novoEvento);
                return StatusCode(201, novoEvento);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Atualiza um evento existente
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Atualizar(Guid id, EventoDTO evento)
        {
            try
            {
                Evento eventoAtualizado = new Evento
                {
                    Nome = evento.Nome!,
                    DataEvento = evento.DataEvento,
                    Descricao = evento.Descricao!,
                    IdTipoEvento = evento.IdTipoEvento,
                    IdInstituicao = evento.IdInstituicao
                };

                _eventoRepository.Atualizar(id, eventoAtualizado);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Deleta um evento do sistema.
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _eventoRepository.Deletar(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
