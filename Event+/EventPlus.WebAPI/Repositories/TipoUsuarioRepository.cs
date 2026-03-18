using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {

        private readonly EventContext _context;
        public TipoUsuarioRepository(EventContext context)
        {
            _context = context;
        }

        public void Atualizar(Guid id, TipoUsuario tipoUsuario)
        {
            var TipoUsuarioBuscado = _context.TipoUsuarios.Find(id);

            if (TipoUsuarioBuscado != null)
            {
                TipoUsuarioBuscado.Titulo = tipoUsuario.Titulo;
                _context.SaveChanges();
            }
        }

        public TipoUsuario BuscarPorId(Guid id)
        {
            return _context.TipoUsuarios.Find(id)!;
        }

        public void Cadastrar(TipoUsuario tipoUsuario)
        {
            _context.TipoUsuarios.Add(tipoUsuario);
            _context.SaveChanges();
        }

        public void Deletar(Guid id)
        {
            var tipousuarioBuscado = _context.TipoUsuarios.Find(id);

            if (tipousuarioBuscado != null)
            {
                _context.TipoUsuarios.Remove(tipousuarioBuscado);
                _context.SaveChanges();
            }
         }


        public List<TipoUsuario> List(Guid IdTipoUsuario)
        {
            return _context.TipoUsuarios.OrderBy(tipoUsuario => tipoUsuario.Titulo).ToList();
        }

        public List<TipoUsuario> Listar()
        {
            return _context.TipoUsuarios.ToList();
        }
    }
}
