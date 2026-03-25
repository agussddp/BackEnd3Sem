using ConnectPlus.BdContextConnect;
using ConnectPlus.DTO;
using ConnectPlus.Interfaces;
using ConnectPlus.Models;
using Microsoft.EntityFrameworkCore;

namespace ConnectPlus.Repositories
{
    public class TipoContatoRepository : ITipoContatoRepository
    {
        private readonly ConnectContext _context;
        public TipoContatoRepository(ConnectContext context)
        {
            _context = context;
        }

        public void Atualizar(Guid Id, TipoContato tipoContato)
        {
            var tipoContatoExistente = _context.TipoContatos.Find(Id);
            if (tipoContatoExistente != null)
            {
                tipoContatoExistente.Titulo = tipoContato.Titulo;
                tipoContatoExistente.Contatos = tipoContato.Contatos;
                _context.SaveChanges();
            }
        }

      
        public TipoContato BuscarPorIdContato(Guid Id)
        {
            return _context.TipoContatos.Find(Id)!;
        }

        public void Cadastrar(TipoContato tipoContato)
        {
            _context.TipoContatos.Add(tipoContato);
            _context.SaveChanges();
        }

        public void Deletar(Guid Id)
        {
            var tipoContatoExistente = _context.TipoContatos.Find(Id);
            if (tipoContatoExistente != null)
            {
                _context.TipoContatos.Remove(tipoContatoExistente);
                _context.SaveChanges();
            }
        }

        public List<TipoContato> Listar()
        {
            return _context.TipoContatos.ToList();
        }
    }
}