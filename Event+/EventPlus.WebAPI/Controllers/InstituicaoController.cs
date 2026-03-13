
using Microsoft.AspNetCore.Mvc;
using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;


namespace EventPlus.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstituicaoController : ControllerBase
    {
        private IInstituicaoRepository _instituicaoRepository;

        public InstituicaoController(IInstituicaoRepository instituicaoRepository)
        {
            _instituicaoRepository = instituicaoRepository;
        }


        /// <summary>
        /// Endpoint da API que faz a chamada para o método de buscar a insticuicao específico
        /// </summary>
        /// <param name="id"></param>
        /// <returns>status code 200 e a instituicao buscada</returns>
        /// 
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(Guid id)
        {
            try
            {
                return Ok(_instituicaoRepository.BuscarPorId(id));
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }



        /// <summary>
        /// Faz a chamada para o método de cadastrar a instituicao
        /// </summary>
        /// <param name="instituicao">instituicao pra cadastro</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Cadastrar(InstituicaoDTO instituicao)
        {
            try
            {
                var novoInstituicao = new Instituicao
                {
                    Endereco = instituicao.Endereco!,
                    NomeFantasia = instituicao.NomeFantasia!,
                    Cnpj = instituicao.Cnpj!
                };

                _instituicaoRepository.Cadastrar(novoInstituicao);
                return StatusCode(201, novoInstituicao);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }



        /// <summary>
        /// Faz a chamada para o método de atualizar a instituicao
        /// </summary>
        /// <param name="id">ID da instituicao atualizado</param>
        /// <param name="instituicao">>instituicao com os dados</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Atualizar(Guid id, InstituicaoDTO instituicao)
        {
            try
            {
                var instituicaoAtualizado = new Instituicao
                {
                    Endereco = instituicao.Endereco!,
                    NomeFantasia = instituicao.NomeFantasia!,
                    Cnpj = instituicao.Cnpj!
                };

                _instituicaoRepository.Atualizar(id, instituicaoAtualizado);
                return StatusCode(204, instituicaoAtualizado);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }



        /// <summary>
        /// Chama do metodo de deletar uma instituicao
        /// </summary>
        /// <param name="id">id da instituicao a ser excluida</param>
        /// <returns>Status code 204</returns>
        [HttpDelete("{id}")]

        public IActionResult Delete(Guid id)
        {
            try
            {
                _instituicaoRepository.Deletar(id);
                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
    }
}
