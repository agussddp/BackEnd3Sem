using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories
{
    public class PresencaRepository : IPresencaRepository
    {

        private readonly EventContext _context;

        public PresencaRepository(EventContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Método que altera a situação da presença
        /// </summary>
        /// <param name="id"> Id da presença a ser alterada</param>
        public void Atualizar(Guid id)
        {
            var presencaBuscada = _context.Presencas.Find(id);

            if (presencaBuscada != null) 
            {
                presencaBuscada.Situacao = !presencaBuscada.Situacao;
                _context.SaveChanges();
            }
        }

        public void Atualizar(Guid id, Evento eventoAtualizado)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Método que busca presença por id
        /// </summary>
        /// <param name="id">Id da presença a ser buscada</param>
        /// <returns>Pesença buscada</returns>
        public Presenca BuscarPorId(Guid id)
        {
            return _context.Presencas.Include(p => p.IdEventoNavigation).ThenInclude(e => e!.IdInstituicaoNavigation).FirstOrDefault(p => p.IdPresenca == id)!;
        }


        /// <summary>
        /// Deleta 
        /// </summary>
        /// <param name="id"></param>
        public void Deletar(Guid id)
        {
            var presencaBuscada = _context.Presencas.Find(id);
            if (presencaBuscada != null)
            {
                _context.Presencas.Remove(presencaBuscada);
                _context.SaveChanges();
            }
        }



        /// <summary>
        /// Cadastra as Presencas
        /// </summary>
        /// <param name="presenca"></param>
        public void Inscrever(Presenca presenca)
        {
            _context.Presencas.Add(presenca);
            _context.SaveChanges();
        }


        /// <summary>
        /// Lista as presencas 
        /// </summary>
        /// <returns></returns>

        public List<Presenca> Listar()
        {
            return _context.Presencas.OrderBy(p => p.IdUsuario).ToList();
        }



        /// <summary>
        /// Método que lista as presenças de um usuário específico
        /// </summary>
        /// <param name="IdUsuario"> Id do usuário para filtragem</param>
        /// <returns>Lista de presença de um usuário</returns>
        public List<Presenca> ListarMinhas(Guid IdUsuario)
        {
            return _context.Presencas.Include(p => p.IdEventoNavigation).ThenInclude(e => e!.IdInstituicaoNavigation).Where(p => p.IdUsuario == IdUsuario).ToList();
        }
    }
}
