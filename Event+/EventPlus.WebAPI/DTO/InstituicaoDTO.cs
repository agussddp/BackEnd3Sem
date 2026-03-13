using System.ComponentModel.DataAnnotations;
namespace EventPlus.WebAPI.DTO;

public class InstituicaoDTO
{
    [Required(ErrorMessage = "O Endereco da Instituicao é obrigatório!")]
    public string? Endereco { get; set; } = null!;

    [Required(ErrorMessage = "O NomeFantasia da Instituicao é obrigatório!")]
    public string? NomeFantasia { get; set; } = null!;

    [Required(ErrorMessage = "O Cnpj da Instituicao é obrigatório!")]
    public string? Cnpj { get; set; } = null!;
}