using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class EventoRepository : IEventoRepository
{
    private readonly EventContext _context;
    public EventoRepository(EventContext context)
    {
        _context = context;
    }
    public void Atualizar(Guid id, Evento evento)
    {
        var EventoBuscado = _context.Eventos.Find(id);
        if (EventoBuscado != null)
        {
            EventoBuscado.Nome = evento.Nome;
            EventoBuscado.Descricao = evento.Descricao;
            EventoBuscado.DataEvento = evento.DataEvento;
            EventoBuscado.IdTipoEventoNavigation = evento.IdTipoEventoNavigation;
            _context.SaveChanges();
        }
    }

    public Evento BuscarPorId(Guid id)
    {
        return _context.Eventos.Find(id)!;
    }

    public void Cadastrar(Evento evento)
    {
        _context.Eventos.Add(evento);
        _context.SaveChanges();
    }

    public void Deletar(Guid id)
    {
        var eventoBuscado = _context.Eventos.Find(id);
        if (eventoBuscado != null)
        {
            _context.Eventos.Remove(eventoBuscado);
            _context.SaveChanges();
        }
    }

    public List<Evento> Listar()
    {
        return _context.Eventos.OrderBy(eventoBuscado => eventoBuscado.IdEvento).ToList();
    }
    /// <summary>
    /// Metodo que lista evento filtrando pela presencas de um usuario
    /// </summary>
    /// <param name="IdUsuario">Id do usuario</param>
    /// <returns>Lista de eventos filtrados por usuário</returns>
    public List<Evento> ListarPorId(Guid IdUsuario)
    {
        return _context.Eventos.Include(e => e.IdTipoEventoNavigation).Include(e => e.IdInstituicaoNavigation).Where(e => e.Presencas.Any(p => p.IdUsuario == IdUsuario && p.Situacao == true)).ToList();
    }
    /// <summary>
    /// Metodo que busca os proximos eventos que irão acontecer
    /// </summary>
    /// <returns>retorna lista de proximos evento</returns>
    public List<Evento> ListarProximos()
    {
        return _context.Eventos.Include(e => e.IdTipoEventoNavigation).Include(e => e.IdInstituicaoNavigation).Where(e => e.DataEvento >= DateTime.Now).OrderBy(e => e.DataEvento).ToList();
    }
}