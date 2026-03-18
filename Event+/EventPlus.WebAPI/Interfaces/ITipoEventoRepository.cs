using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Interfaces;

public interface ITipoEventoRepository
{
    void Cadastrar(TipoEvento tipoEvento);
    void Deletar(Guid id);
    TipoEvento BuscarPorId(Guid id);
    List<TipoEvento> Listar();
    void Atualizar(Guid id, TipoEvento tipoEvento);
}
