using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories
{
    /// <summary>
    /// Repositório responsável pela manipulação da entidade Evento no banco de dados.
    /// </summary>
    public class EventoRepository : IEventoRepository
    {
        private readonly EventContext _context;

        public EventoRepository(EventContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Cadastra um novo evento no sistema
        /// </summary>
        /// <param name="evento">Objeto contendo os dados do evento a ser cadastrado</param>
        public void Cadastrar(Evento evento)
        {
            _context.Eventos.Add(evento);
            _context.SaveChanges();
        }

        /// <summary>
        /// Lista todos os eventos cadastrados, incluindo dados de TipoEvento e Instituição
        /// </summary>
        /// <returns>Uma lista de objetos do tipo Evento</returns>
        public List<Evento> Listar()
        {
            return _context.Eventos.Include(e => e.IdTipoEventoNavigation).Include(e => e.IdInstituicaoNavigation).ToList();
        }

        /// <summary>
        /// Busca um evento específico por seu identificador único
        /// </summary>
        /// <param name="id">ID (Guid) do evento desejado</param>
        /// <returns>O objeto Evento encontrado ou null caso não exista</returns>
        public Evento BuscarPorId(Guid id)
        {
            return _context.Eventos.Include(e => e.IdTipoEventoNavigation).Include(e => e.IdInstituicaoNavigation).FirstOrDefault(e => e.IdEvento == id)!;
        }

        /// <summary>
        /// Atualiza as informações de um evento existente
        /// </summary>
        /// <param name="id">ID do evento que será atualizado</param>
        /// <param name="evento">Objeto contendo as novas informações</param>
        public void Atualizar(Guid id, Evento evento)
        {
            var eventoBuscado = _context.Eventos.Find(id);

            if (eventoBuscado != null)
            {
                eventoBuscado.Nome = evento.Nome;
                eventoBuscado.DataEvento = evento.DataEvento;
                eventoBuscado.Descricao = evento.Descricao;

                _context.Eventos.Update(eventoBuscado);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Remove um evento do banco de dados pelo seu ID
        /// </summary>
        /// <param name="id">ID do evento a ser deletado</param>
        public void Deletar(Guid id)
        {
            var eventoBuscado = _context.Eventos.Find(id);
            if (eventoBuscado != null)
            {
                _context.Eventos.Remove(eventoBuscado);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Lista os eventos em que um usuário específico confirmou presença
        /// </summary>
        /// <param name="idUsuario">ID do usuário para filtragem</param>
        /// <returns>Lista de eventos relacionados à presença do usuário</returns>
        public List<Evento> ListarPorId(Guid idUsuario)
        {
            return _context.Eventos.Include(e => e.IdTipoEventoNavigation).Include(e => e.IdInstituicaoNavigation).Where(e => e.Presencas.Any(p => p.IdUsuario == idUsuario && p.Situacao == true)).ToList();
        }

        /// <summary>
        /// Lista os eventos que ainda não ocorreram data maior ou igual à atual
        /// </summary>
        /// <returns>Lista de eventos futuros ordenada por data</returns>
        public List<Evento> ListarProximos()
        {
            return _context.Eventos
                .Include(e => e.IdTipoEventoNavigation).Include(e => e.IdInstituicaoNavigation).Where(e => e.DataEvento >= DateTime.Now).OrderBy(e => e.DataEvento).ToList();
        }
    }
}
