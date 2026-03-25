using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO
{
    public class EventoDTO
    {
        public string? Nome { get; set; }
        [Required(ErrorMessage = "blupp")]
        public string? Descricao { get; set; }
        [Required(ErrorMessage = "blupp")]
        public DateTime DataEvento { get; set; }
        [Required(ErrorMessage = "blupp")]
        public Guid IdTipoEvento { get; set; }
        [Required(ErrorMessage = "blupp")]
        public Guid IdInstituicao { get; set; }
    }
}