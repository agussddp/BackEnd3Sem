using EventPlus.WebAPI.DTOs;
using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Interfaces
{
    public interface IPresencaRepository
    {
        void Inscrever(Presenca presenca);
        void Deletar(Guid id);
        List<Presenca> Listar();
        Presenca BuscarPorId(Guid id);
        void Atualizar(Guid id, Evento eventoAtualizado);
        List<Presenca> ListarMinhas(Guid IdUsuario);
        void Atualizar(Guid id, PresencaDTO presenca);
    }
}
