using EventPlus.WebAPI.Models;
namespace EventPlus.WebAPI.Interfaces;

public interface IComentarioEventoRepository
{
    void Cadastrar(ComentarioEvento comentarioEvento);
    void Deletar(Guid idComentarioEvento);
    List<ComentarioEvento> Listar();
    ComentarioEvento BuscarPorIdUsuario(Guid IdUsuario);
    List<ComentarioEvento> ListarSomenteExibe(Guid IdEvento);

}