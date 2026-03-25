using EventPlus.WebAPI.Models;
namespace EventPlus.WebAPI.Interfaces;

public interface IPresencaRepository
{
    void Inscrever(Presenca presenca);
    void Deletar(Guid id);
    void Atualizar(Guid id, Presenca presenca);
    List<Presenca> Listar();
    Presenca BuscarPorId(Guid id);
    List<Presenca> ListarMinhas(Guid IdUsuario);
}