using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Interfaces;

public interface IEventoRepository
{
    void Cadastrar(Evento evento);
    void Deletar(Guid id);
    void Atualizar(Guid id, Evento evento);
    List<Evento> Listar();
    Evento BuscarPorId(Guid id);
    List<Evento> ListarPorId(Guid IdUsuario);
    List<Evento> ListarProximos();
}