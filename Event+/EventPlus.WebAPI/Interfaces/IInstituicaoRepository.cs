using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Interfaces
{
    public interface IInstituicaoRepository
    {
        void Cadastrar(Instituicao instituicao);
        void Deletar(Guid id);
        void Atualizar(Guid id, Instituicao instituicao);
        List<Instituicao> Listar();
        Instituicao BuscarPorId(Guid id);
    }
}