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

        /// <summary>
        /// Construtor da classe TipoContatoRepository.
        /// </summary>
        /// <param name="context"></param>
        public TipoContatoRepository(ConnectContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Atualiza as informações de um tipo de contato existente
        /// </summary>
        /// <param name="Id">Identificador único do tipo de contato a ser atualizado</param>
        /// <param name="tipoContato">Objeto contendo os novos dados para atualização</param>
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

        /// <summary>
        /// Busca um tipo de contato específico através do seu ID
        /// </summary>
        /// <param name="Id">Identificador único do tipo de contato</param>
        /// <returns>O contato que foi encontrado</returns>
        public TipoContato BuscarPorIdContato(Guid Id)
        {
            return _context.TipoContatos.Find(Id)!;
        }

        /// <summary>
        /// Cadastra um novo tipo de contato 
        /// </summary>
        /// <param name="tipoContato">tipo de contato a ser cadastrado</param>
        public void Cadastrar(TipoContato tipoContato)
        {
            _context.TipoContatos.Add(tipoContato);
            _context.SaveChanges();
        }

        /// <summary>
        /// Remove um tipo de contato 
        /// </summary>
        /// <param name="Id">Identificador único do tipo de contato a ser excluído</param>
        public void Deletar(Guid Id)
        {
            var tipoContatoExistente = _context.TipoContatos.Find(Id);
            if (tipoContatoExistente != null)
            {
                _context.TipoContatos.Remove(tipoContatoExistente);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Lista todos os tipos de contato cadastrados
        /// </summary>
        /// <returns>Uma lista do Tipo de contato</returns>
        public List<TipoContato> Listar()
        {
            return _context.TipoContatos.ToList();
        }
    }
}
