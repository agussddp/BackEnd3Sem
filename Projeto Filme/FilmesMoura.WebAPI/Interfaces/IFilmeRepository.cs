using FilmesMoura.WebAPI.Models;

namespace FilmesMoura.WebAPI.Interfaces;

public interface IFilmeRepository
{
    void Cadastrar(FIlme novoFilme);
    List<FIlme> Listar();
    void AtualizarIdCorpo(FIlme FIlmeAtualizado);
    void AtualizarIdUrl(Guid id, FIlme FIlmeAtualizado);
    void Deletar(Guid id);
    FIlme BuscarPorId(Guid id);
}
