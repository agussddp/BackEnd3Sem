

using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTO;

public class LoginDTO
{
    [Required(ErrorMessage = "O Email do Usuario é obrigatório!")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "A Senha do Usuario é obrigatório!")]
    public string? Senha { get; set; }
}
