using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Interfaces
{
    public interface IInstituicaoRepository
    {
        void Cadastrar(Instituicao instituicao);
        void Deletar(Guid id);
        List<Presenca> Listar();
        List<Instituicao> List(Guid IdInstituicao);
        Instituicao BuscarPorId(Guid IdInstituicao);
        void Atualizar(Guid id, Instituicao instituicao);
    }
}
