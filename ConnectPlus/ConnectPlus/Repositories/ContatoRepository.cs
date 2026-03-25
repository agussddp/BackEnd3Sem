using ConnectPlus.BdContextConnect;
using ConnectPlus.Interfaces;
using ConnectPlus.Models;

namespace ConnectPlus.Repositories
{
    public class ContatoRepository : IContatoRepository
    {
        private readonly ConnectContext _context;
        public ContatoRepository(ConnectContext context)
        {
            _context = context;
        }
        public void Atualizar(Guid Id, Contato contato)
        {
            var contatoExistente = _context.Contatos.Find(Id);
            if (contatoExistente != null)
            {
                contatoExistente.Nome = contato.Nome;
                contatoExistente.FormaDeContato = contato.FormaDeContato;
                contatoExistente.Imagem = contato.Imagem;
                contatoExistente.IdTipoContato = contato.IdTipoContato;
                _context.SaveChanges();
            }
        }

        public Contato BuscarPorId(Guid id)
        {
            throw new NotImplementedException();
        }

        public Contato BuscarPorIdContato(Guid Id)
        {
            return _context.Contatos.Find(Id)!;
        }

        public void Cadastrar(Contato contato)
        {
            _context.Contatos.Add(contato);
            _context.SaveChanges();
        }

        public void Deletar(Guid Id)
        {
            var contatoExistente = _context.Contatos.Find(Id);
            if (contatoExistente != null)
            {
                _context.Contatos.Remove(contatoExistente);
                _context.SaveChanges();
            }
        }

        public List<Contato> Listar()
        {
            return _context.Contatos.ToList();
        }
    }
}