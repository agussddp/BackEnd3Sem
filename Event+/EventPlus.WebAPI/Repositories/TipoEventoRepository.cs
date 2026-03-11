using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Repositories
{
    public class TipoEventoRepository : ITipoEventoRepository
    {
        private readonly EventContext _context;

        //Injecao de dependencia: recebe o contexto pelo construtor
        public TipoEventoRepository(EventContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Atualiza um tipo de evento usando um rastreamento automatico
        /// </summary>
        /// <param name="id"></id fo tipo evento a seratualizado</param>
        /// <param name="tipoEvento">Novos dados do tipo evento</param>

        public void Atualizar(Guid id, TipoEvento tipoEvento)
        {
            var tipoEventoBuscado = _context.TipoEventos.Find(id);

            if (tipoEventoBuscado != null)
            {
                tipoEventoBuscado.Titulo = tipoEvento.Titulo;
                //O SaveChanges detecta as mudancas na propriedade titulo automaticamente
                _context.SaveChanges();
            }
        }


        /// <summary>
        /// Busca um tipo de evento por id
        /// </summary>
        /// <param name="id">id do evento a ser buscado</param>
        /// <returns>objeto do tipoEvento com as informacoes do tipo evento buscado</returns>
        public TipoEvento BuscarPorId(Guid id)
        {
            return _context.TipoEventos.Find(id)!;
        }


        /// <summary>
        /// Cadastra um novo tipo de evento
        /// </summary>
        /// <param name="tipoEvento"></param>
        public void Cadastrar(TipoEvento tipoEvento)
        {
            _context.TipoEventos.Add(tipoEvento);
            _context.SaveChanges();
        }


        /// <summary>
        /// Deleta um tipo de evento
        /// </summary>
        /// <param name="id">id do tipo de evento a ser deletado</param>
        public void Deletar(Guid id)
        {
            var tipoEventoBuscado = _context.TipoEventos.Find(id);

            if(tipoEventoBuscado != null)
            {
                _context.TipoEventos.Remove(tipoEventoBuscado);
                _context.SaveChanges();
            }
        }


        /// <summary>
        /// Busca a lista de tipo de eventos cadastrados
        /// </summary>
        /// <returns>Uma lista de tipos de eventos</returns>

        public List<TipoEvento> Listar()
        {
            return _context.TipoEventos.OrderBy(tipoEvento => tipoEvento.Titulo).ToList();
        }
    }
}
