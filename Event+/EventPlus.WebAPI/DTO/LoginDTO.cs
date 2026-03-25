using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "O nome de usuario é obrigatorio!")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "A Senha do tipo de evento é obrigatório")]
        public string? Senha { get; set; }

        [Required(ErrorMessage = "Informe seu tipo de usuario!")]
        public string? Titulo { get; set; }

    }
}
