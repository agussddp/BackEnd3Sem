using ConnectPlus.DTO;
using ConnectPlus.Interfaces;
using ConnectPlus.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConnectPlus.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContatoController : ControllerBase
{
    private readonly IContatoRepository _contatoRepository;

    public ContatoController(IContatoRepository contatoRepository)
    {
        _contatoRepository = contatoRepository;
    }

    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            return Ok(_contatoRepository.Listar());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        try
        {
            var contato = _contatoRepository.BuscarPorId(id);
            if (contato == null) return NotFound("Contato não encontrado");

            return Ok(contato);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromForm] ContatoDTO novoContato)
    {
        // Validação básica baseada no seu exemplo
        if (string.IsNullOrEmpty(novoContato.Nome) || novoContato.IdTipoContato == Guid.Empty)
            return BadRequest("É obrigatório que o contato tenha nome e tipo de usuário");

        Contato contato = new Contato();
        contato.Idusuario = Guid.NewGuid();
        contato.Nome = novoContato.Nome;
        contato.FormaDeContato = novoContato.FormaDeContato;
        contato.IdTipoContato = novoContato.IdTipoContato;

        if (novoContato.Imagem != null && novoContato.Imagem.Length > 0)
        {
            var extensao = Path.GetExtension(novoContato.Imagem.FileName);
            var nomeArquivo = $"{Guid.NewGuid()}{extensao}";

            var pastaRelativa = "wwwroot/imagens";
            var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);

            if (!Directory.Exists(caminhoPasta))
                Directory.CreateDirectory(caminhoPasta);

            var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);

            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                await novoContato.Imagem.CopyToAsync(stream);
            }

            contato.Imagem = nomeArquivo;
        }

        try
        {
            _contatoRepository.Cadastrar(contato);
            return StatusCode(201);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromForm] ContatoDTO contato)
    {
        var contatoBuscado = _contatoRepository.BuscarPorId(id);

        if (contatoBuscado == null)
            return NotFound("Contato não encontrado");

        // Atualização de campos simples
        if (!string.IsNullOrWhiteSpace(contato.Nome))
            contatoBuscado.Nome = contato.Nome;

        if (!string.IsNullOrWhiteSpace(contato.FormaDeContato))
            contatoBuscado.FormaDeContato = contato.FormaDeContato;

        if (contato.IdTipoContato != null && contato.IdTipoContato != Guid.Empty)
            contatoBuscado.IdTipoContato = contato.IdTipoContato;

        // Lógica de imagem (Substituição)
        if (contato.Imagem != null && contato.Imagem.Length != 0)
        {
            var pastaRelativa = "wwwroot/imagens";
            var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);

            // Deletar arquivo antigo se existir
            if (!string.IsNullOrEmpty(contatoBuscado.Imagem))
            {
                var caminhoAntigo = Path.Combine(caminhoPasta, contatoBuscado.Imagem);
                if (System.IO.File.Exists(caminhoAntigo))
                    System.IO.File.Delete(caminhoAntigo);
            }

            // Salva a nova imagem
            var extensao = Path.GetExtension(contato.Imagem.FileName);
            var nomeArquivo = $"{Guid.NewGuid()}{extensao}";

            if (!Directory.Exists(caminhoPasta))
                Directory.CreateDirectory(caminhoPasta);

            var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);
            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                await contato.Imagem.CopyToAsync(stream);
            }

            contatoBuscado.Imagem = nomeArquivo;
        }

        try
        {
            _contatoRepository.Atualizar(id, contatoBuscado);
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
        var contatoBuscado = _contatoRepository.BuscarPorId(id);
        if (contatoBuscado == null)
            return NotFound("Contato não encontrado");

        // Deletar arquivo físico da imagem antes de remover do banco
        if (!string.IsNullOrEmpty(contatoBuscado.Imagem))
        {
            var pastaRelativa = "wwwroot/imagens";
            var caminho = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa, contatoBuscado.Imagem);

            if (System.IO.File.Exists(caminho))
                System.IO.File.Delete(caminho);
        }

        try
        {
            _contatoRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
}