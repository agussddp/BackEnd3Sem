using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Utils;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositorios;

public class UsuarioRepository : IUsuarioRepository
{

    private readonly EventContext _context;

    public UsuarioRepository(EventContext context)
    {
        _context = context;
    }
    /// <summary>
    /// Busca o usuario pelo email e valida o hash pela senha 
    /// </summary>
    /// <param name="Email">Email do Usuario</param>
    /// <param name="Senha">Senha do Usuario</param>
    /// <returns>Retorna o Usuario buscado e validado</returns>
    public Usuario BuscarPorEmailESenha(string Email, string Senha, string Titulo)
    {
        //Primeiro Buscamos o Usuario Pelo Email
        var usuarioBuscado = _context.Usuarios.Include(usuario => usuario.IdTipoUsuarioNavigation).FirstOrDefault(usuario => usuario.Email == Email);
        //Verifica se o usuario realmente exite
        if (usuarioBuscado != null)
        {
            //comparamos o hash da senha com oq ta no banco de dados
            bool confere = Criptografia.CompararHash(Senha, usuarioBuscado.Senha);

            if (confere)
            {
                return usuarioBuscado;
            }
        }
        return null!;
    }
    /// <summary>
    /// Busca um usuario pelo id incluindo os dado do seu tipo usuario 
    /// </summary>
    /// <param name="IdUsuario">Id do usuario a ser buscado</param>
    /// <returns>Usuario buscado</returns>
    public Usuario BuscarPorId(Guid IdUsuario)
    {
        return _context.Usuarios.Include(usuario => usuario.IdTipoUsuarioNavigation!).FirstOrDefault(usuario => usuario.IdUsuario == IdUsuario)!;
    }

    /// <summary>
    /// Cadastra um novo usuario no banco de dados, criptografando a senha antes de salvar
    /// </summary>
    /// <param name="usuario">Recebe o usuario a ser cadastrado</param>
    public void Cadastrar(Usuario usuario)
    {
        usuario.Senha = Criptografia.GerarHash(usuario.Senha);
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
    }
    public List<Usuario> Listar()
    {
        return _context.Usuarios.OrderBy(usuario => usuario.IdUsuario).ToList();
    }
}