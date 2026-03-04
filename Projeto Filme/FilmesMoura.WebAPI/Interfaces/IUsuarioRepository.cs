using FilmesMoura.WebAPI.Models;

namespace WebApplication1.Interface;

public interface IUsuarioRepository
{
    void Cadastrar(Usuario novoUsuario);
    Usuario BuscarPorId(Guid id);
    Usuario BuscarPorEmailESenha(String email, string senha);
}
