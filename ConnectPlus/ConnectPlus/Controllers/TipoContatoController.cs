using ConnectPlus.DTO;
using ConnectPlus.Interfaces;
using ConnectPlus.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConnectPlus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoContatoController : ControllerBase
    {
        private readonly ITipoContatoRepository _tipoContatoRepository;
        public TipoContatoController(ITipoContatoRepository tipoContatoRepository)
        {
            _tipoContatoRepository = tipoContatoRepository;
        }
        [HttpPost]
        public IActionResult Cadastrar(TipoContatoDTO tipoContatoDTO)
        {
            try
            {
                var tipoContato = new TipoContato
                {
                    Titulo = tipoContatoDTO.Titulo
                };
                _tipoContatoRepository.Cadastrar(tipoContato);
                return Ok("Tipo de contato cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var tipoContatos = _tipoContatoRepository.Listar();
                return Ok(tipoContatos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public IActionResult Atualizar(Guid id, TipoContatoDTO tipoContatoDTO)
        {
            try
            {
                var tipoContato = new TipoContato
                {
                    Titulo = tipoContatoDTO.Titulo
                };
                _tipoContatoRepository.Atualizar(id, tipoContato);
                return Ok("Tipo de contato atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Deletar(Guid id)
        {
            try
            {
                _tipoContatoRepository.Deletar(id);
                return Ok("Tipo de contato deletado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public IActionResult BuscarPorIdContato(Guid id)
        {
            try
            {
                var tipoContato = _tipoContatoRepository.BuscarPorIdContato(id);
                if (tipoContato == null)
                {
                    return NotFound("Tipo de contato não encontrado.");
                }
                return Ok(tipoContato);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}