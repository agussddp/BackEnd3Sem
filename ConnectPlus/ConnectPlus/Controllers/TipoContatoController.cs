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

        /// <summary>
        /// Realiza o cadastro de um novo tipo de contato
        /// </summary>
        /// <param name="tipoContatoDTO">Objeto de transferência de dados</param>
        /// <returns>Uma mensagem de sucesso ou erro em caso de falha</returns>
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

        /// <summary>
        /// Lista todos os tipos de contato cadastrados 
        /// </summary>
        /// <returns>Uma lista de tipos de contato ou uma mensagem de erro.</returns>
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

        /// <summary>
        /// Atualiza as informações de um tipo de contato existente
        /// </summary>
        /// <param name="id">O ID único do tipo de contato a ser atualizado</param>
        /// <param name="tipoContatoDTO">Novos dados para o tipo de contato</param>
        /// <returns>Mensagem de confirmação da atualização ou erro</returns>
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

        /// <summary>
        /// Remove um tipo de contato do sistema através do ID
        /// </summary>
        /// <param name="id">O ID do tipo de contato a ser deletado</param>
        /// <returns>Mensagem de confirmação da exclusão ou erro</returns>
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

        /// <summary>
        /// Busca os detalhes de um tipo de contato específico pelo seu ID
        /// </summary>
        /// <param name="id">O id único do tipo de contato</param>
        /// <returns></returns>
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
