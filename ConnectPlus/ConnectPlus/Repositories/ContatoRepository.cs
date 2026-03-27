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

        /// <summary>
        /// Atualiza os dados de um contato existente no banco de dados
        /// </summary>
        /// <param name="Id">O identificador único do contato a ser atualizado</param>
        /// <param name="contato"> as novas informações do contato</param>
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

        /// <summary>
        /// Busca um contato pelo seu identificador 
        /// </summary>
        /// <param name="id">O ID do contato</param>
        /// <returns>Id do contato</returns>
        public Contato BuscarPorId(Guid id)
        {
            return _context.Contatos.Find(id)!;
        }

        /// <summary>
        /// Busca um contato específico através do seu id
        /// </summary>
        /// <param name="Id">O id do contato</param>
        /// <returns>O contato encontrado</returns>
        public Contato BuscarPorIdContato(Guid Id)
        {
            return _context.Contatos.Find(Id)!;
        }

        /// <summary>
        /// Realiza o cadastro de um novo contato
        /// </summary>
        /// <param name="contato">contato a ser salvo</param>
        public void Cadastrar(Contato contato)
        {
            _context.Contatos.Add(contato);
            _context.SaveChanges();
        }

        /// <summary>
        /// Exclui um contato 
        /// </summary>
        /// <param name="Id">O id do contato que será removido</param>
        public void Deletar(Guid Id)
        {
            var contatoExistente = _context.Contatos.Find(Id);
            if (contatoExistente != null)
            {
                _context.Contatos.Remove(contatoExistente);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Lista todos os contatos registrados no sistema
        /// </summary>
        /// <returns>Uma lista contendo todos os contatos</returns>
        public List<Contato> Listar()
        {
            return _context.Contatos.ToList();
        }
    }
}
