using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO;

public class UsuarioDTO
{

    [Required(ErrorMessage = "O nome de usuario é obrigatorio!")]
    public string? Nome { get; set; }
    [Required(ErrorMessage = "O Email do usuario é obrigatório")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "A Senha do tipo de evento é obrigatório")]
    public string? Senha { get; set; }
    public Guid IdTipoUsuario { get; set; }
}