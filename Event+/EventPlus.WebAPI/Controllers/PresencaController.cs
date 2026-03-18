using EventPlus.WebAPI.DTOs;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PresencaController : ControllerBase
    {
        private IPresencaRepository _presencaRepository;

        public PresencaController(IPresencaRepository presencaRepository) 
        {
            _presencaRepository = presencaRepository;
        }



        /// <summary>
        /// Endpoint  da API que retorna uma lista de presenças de um usuário específico
        /// </summary>
        /// <param name="idUsuario"> Id do usuário para filtragem</param>
        /// <returns>status code 200 e uma lista de presença</returns>
        [HttpGet("ListarMinhas/{idUsuario}")]
        public IActionResult BuscarMinhas(Guid idUsuario) 
        {
            try
            {
                return Ok(_presencaRepository.ListarMinhas(idUsuario));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Inscrever(PresencaDTO presenca) 
        {
            try
            {
                var novaPresenca = new Presenca
                {
                    Situacao = presenca.Situacao,
                    IdEvento = presenca.IdEvento,
                    IdUsuario = presenca.IdUsuario,
                };

                _presencaRepository.Inscrever(novaPresenca);
                return StatusCode(201,novaPresenca);
            }
            catch (Exception)
            {
                throw;
            }
        }



        [HttpPut("{id}")]
        public IActionResult Atualizar(Guid id, PresencaDTO presenca)
        {
            try
            {
                Presenca presencaAtualizada = new Presenca
                {
                    Situacao = presenca.Situacao!,
                    IdUsuario = presenca.IdUsuario,
                    IdEvento = presenca.IdEvento!
                };

                _presencaRepository.Atualizar(id, presenca);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _presencaRepository.Deletar(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
