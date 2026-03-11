using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Repositories
{
    public class InstituicaoRepository : IInstituicaoRepository
    {
        private readonly EventContext _context;

        public InstituicaoRepository(EventContext context)
        {
            _context = context;
        }

        public void Atualizar(Guid id, Instituicao instituicao)
        {
            var instituicaoBuscada = _context.Instituicaos.Find(id);

            if (instituicaoBuscada != null)
            {
                instituicaoBuscada.NomeFantasia = instituicao.NomeFantasia;
                instituicaoBuscada.Endereco = instituicao.Endereco;
                instituicaoBuscada.Cnpj = instituicao.Cnpj;

                // Não é necessario chamar o _context.Update de o objeto foi buscado no mesmo contexto
                _context.SaveChanges();
            }
        }

        public Instituicao BuscarPorId(Guid id) 
        {
            return _context.Instituicaos.Find(id)!;
        }

        public void Cadastrar(Instituicao instituicao)
        {
            _context.Instituicaos.Add(instituicao);
            _context.SaveChanges();
        }

        public void Deletar(Guid id)
        {
            var instituicaoBuscada = _context.Instituicaos.Find(id);

            if (instituicaoBuscada != null)
            {
                _context.Instituicaos.Remove(instituicaoBuscada);
                _context.SaveChanges();
            }
        }

        public List<Instituicao> List(Guid IdInstituicao)
        {
            throw new NotImplementedException();
        }

        public List<Instituicao> Listar() // Removido parâmetro desnecessário para listagem geral
        {
            return _context.Instituicaos.OrderBy(instituicao => instituicao.NomeFantasia).ToList();
        }
    }
}
