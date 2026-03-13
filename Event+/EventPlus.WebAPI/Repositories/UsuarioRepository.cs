using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Utils;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly EventContext _context;
        public UsuarioRepository(EventContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Busca o usuario pelo email e valida o hash da senha
        /// </summary>
        /// <param name="Email">email do usuario</param>
        /// <param name="Senha">senha do usuario</param>
        /// <returns>Usuario buscado e valido</returns>
        public Usuario BuscarPorEmailESenha(string Email, string Senha)
        {
            //Primeiro, buscamos o usúario pelo e-mail
            var usuarioBuscado = _context.Usuarios.Include(usuario => usuario.IdTipoUsuarioNavigation).FirstOrDefault(usuario => usuario.Email == Email);

            //Verificar se o usuario realmente existe
            if (usuarioBuscado != null)
            {
                bool confere = Criptografia.CompararHash(Senha, usuarioBuscado.Senha);

                if (confere)
                {
                    return usuarioBuscado;
                }
            }

            return null;

        }



        /// <summary>
        /// Busca usuario pelo id, incluindo o tipo usuario
        /// </summary>
        /// <param name="IdUsuario">Id do usuario a ser buscado </param>
        /// <returns>usuario buscado</returns>

        public Usuario BuscarPorId(Guid IdUsuario)
        {
            return _context.Usuarios.Include(usuario => usuario.IdTipoUsuarioNavigation).FirstOrDefault(usuario => usuario.IdUsuario == IdUsuario)!;
        }


        /// <summary>
        /// Cadastra um novo usuario com a senha criptogrefada
        /// </summary>
        /// <param name="usuario">usuario a ser cadastrado</param>
        public void Cadastrar(Usuario usuario)
        {
            usuario.Senha = Criptografia.GerarHash(usuario.Senha);

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }
    }
}