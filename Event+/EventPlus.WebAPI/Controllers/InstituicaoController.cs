using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstituicaoController : ControllerBase
    {
        private IInstituicaoRepository _InstituicaoRepository;
        //injeção de dependencia
        public InstituicaoController(IInstituicaoRepository institucaoRepository)
        {
            _InstituicaoRepository = institucaoRepository;
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
                return Ok(_InstituicaoRepository.Listar());
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
                return Ok(_InstituicaoRepository.BuscarPorId(id));
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
        public IActionResult Cadastrar(InstituicaoDTO instituicao)
        {
            try
            {
                var novainstituicao = new Instituicao
                {
                    NomeFantasia = instituicao.NomeFantasia!,
                    Cnpj = instituicao.Cnpj!,
                    Endereco = instituicao.Endereco!,
                    Eventos = new List<Evento>()
                };
                _InstituicaoRepository.Cadastrar(novainstituicao);
                return StatusCode(201, novainstituicao);

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
        public IActionResult Atualizar(Guid id, InstituicaoDTO instituicao)
        {
            try
            {
                var novainstituicao = new Instituicao
                {
                    NomeFantasia = instituicao.NomeFantasia!,
                    Cnpj = instituicao.Cnpj!,
                    Endereco = instituicao.Endereco!,
                    Eventos = new List<Evento>()
                };
                _InstituicaoRepository.Atualizar(id, novainstituicao);
                return StatusCode(204, instituicao);
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
                _InstituicaoRepository.Deletar(id);
                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }


    }

}
