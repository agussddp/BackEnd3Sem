using ConnectPlus.Models;

namespace ConnectPlus.DTO
{
        public interface ITipoContatoRepository
        {
            void Cadastrar(TipoContato tipoContato);
            void Deletar(Guid Id);
            void Atualizar(Guid Id, TipoContato tipoContato);
            List<TipoContato> Listar();
            TipoContato BuscarPorIdContato(Guid Id);
        }
    
        }

