using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebApi.Repositories;

public class ComentarioEventoRepository : IComentarioEventoRepository
{
    private readonly EventContext _context;

    public ComentarioEventoRepository(EventContext context)
    {
        _context = context;
    }

    public ComentarioEvento BuscarPorIdUsuario(Guid idUsuario)
    {
        return _context.ComentarioEventos.FirstOrDefault(c => c.IdUsuario == idUsuario)!;
    }
    public void Cadastrar(ComentarioEvento comentarioEvento)
    {
        _context.ComentarioEventos.Add(comentarioEvento);
        _context.SaveChanges();
    }

    public void Deletar(Guid idComentarioEvento)
    {
        var comentarioEventoBuscado = _context.ComentarioEventos.Find(idComentarioEvento);

        if (comentarioEventoBuscado != null)
        {
            _context.ComentarioEventos.Remove(comentarioEventoBuscado);
            _context.SaveChanges();
        }
    }
    public List<ComentarioEvento> Listar()
    {
        return _context.ComentarioEventos.OrderBy(ComentarioEvento => ComentarioEvento.Descricao).ToList();
    }
    public List<ComentarioEvento> ListarSomenteExibe(Guid IdEvento)
    {
        return _context.ComentarioEventos
            .Include(u => u.IdUsuarioNavigation)
            .Where(c => c.ExibeComentario == true && c.IdEvento == IdEvento)
            .OrderBy(c => c.Descricao)
            .ToList()!;
    }
}