namespace EventPlus.WebAPI.DTOs
{
    public class PresencaDTO
    {

        public bool Situacao { get; set; }
        public Guid? IdUsuario { get; set; }
        public Guid? IdEvento { get; set; }

        
    }
}
