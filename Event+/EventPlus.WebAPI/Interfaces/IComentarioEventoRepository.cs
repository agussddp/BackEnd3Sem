using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Interfaces;

public interface IComentarioEventoRepository
{
    void Cadastrar(ComentarioEvento comentarioEvento);
    void Deletar(Guid IdComentarioEvento);
    List<ComentarioEvento> List(Guid IdEvento);
    List<ComentarioEvento> Listar();
    
    ComentarioEvento BuscarPorIdUsuario(Guid IdUsuario, Guid IdEvento);
    List<ComentarioEvento> ListarSomenteExibe(Guid IdEvento);
}
