using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {
        private ITipoUsuarioRepository _tipoUsuarioRepository;


        public TipoUsuarioController(ITipoUsuarioRepository tipoUsuarioRepository)
        {
            _tipoUsuarioRepository = tipoUsuarioRepository;
        }


        /// <summary>
        /// Endpoint da API que faz a chamada para o método de buscar o tipo de evento específico
        /// </summary>
        /// <param name="id"></param>
        /// <returns>status code 200 e o tipo de usuario buscado</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(Guid id)
        {
            try
            {
                return Ok(_tipoUsuarioRepository.BuscarPorId(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        
        
        /// <summary>
        /// Faz a chamada para o método de cadastrar um tipo de usuario
        /// </summary>
        /// <param name="tipoUsuario">Tipo de usuario pra cadastro</param>
        /// <returns>Status code 201 e o tipo de usuario a ser cadastrado</returns>
        [HttpPost]
        public IActionResult Cadastrar(TipoUsuario tipoUsuario)
        {
            try
            {
                _tipoUsuarioRepository.Cadastrar(tipoUsuario);
                return StatusCode(201, tipoUsuario);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        /// <summary>
        /// Faz a chamada para o método de atualizar um tipo de usuario
        /// </summary>
        /// <param name="id"> ID Do tipo de usuario atualizado</param>
        /// <param name="tipoUsuario">tipo de usuario com os dados</param>
        /// <returns>Status Code 204 e o tipo de usuario atualizados</returns>
        [HttpPut("{id}")]
        public IActionResult Atualizar(Guid id, TipoUsuario tipoUsuario)
        {
            try
            {
                var tipoEventoAtualizado = new TipoEvento
                {
                    Titulo = tipoUsuario.Titulo!
                };
                _tipoUsuarioRepository.Atualizar(id, tipoUsuario);

                return StatusCode(204, tipoUsuario);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// faz a chamada para o método de deletar um tipo de usuario
        /// </summary>
        /// <param name="id">Id do tipo de usuario a ser excluido</param>
        /// <returns>Status code 204</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _tipoUsuarioRepository.Deletar(id);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
