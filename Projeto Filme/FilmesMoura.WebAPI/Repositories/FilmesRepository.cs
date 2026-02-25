using FilmesMoura.WebAPI.BdContextFilme;
using FilmesMoura.WebAPI.Interfaces;
using FilmesMoura.WebAPI.Models;

namespace FilmesMoura.WebAPI.Repositories;

public class FilmesRepository : IFilmeRepository
{
    private readonly FilmeContext _context;

    public FilmesRepository(FilmeContext context)
    {
        _context = context;
    }

    public void AtualizarIdCorpo(FIlme FIlmeAtualizado)
    {
        try
        {
            FIlme filmeBuscado = _context.FIlmes.Find(FIlmeAtualizado.IdFilme);
            if (filmeBuscado == null) 
            {
                filmeBuscado.Tiulo = FIlmeAtualizado.Tiulo;
                filmeBuscado.IdGenero = FIlmeAtualizado.IdGenero;
            }

            _context.FIlmes.Update(filmeBuscado!);
            _context.SaveChanges();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void AtualizarIdUrl(Guid id, FIlme FIlmeAtualizado)
    {
        try
        {
            FIlme filmeBuscado = _context.FIlmes.Find(id.ToString());

            if (filmeBuscado != null) 
            {
                filmeBuscado.Tiulo = FIlmeAtualizado.Tiulo;
                filmeBuscado.IdGenero = FIlmeAtualizado.IdGenero;
            }

            _context.FIlmes.Update(filmeBuscado!);
            _context.SaveChanges();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public FIlme BuscarPorId(Guid id)
    {
        try
        {
            FIlme filmeBuscado = _context.FIlmes.Find(id.ToString())!;
            return filmeBuscado;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void Cadastrar(FIlme novoFilme)
    {
        try
        {
            novoFilme.IdFilme = Guid.NewGuid().ToString();

            _context.FIlmes.Add(novoFilme);
            _context.SaveChanges();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void Deletar(Guid id)
    {
        throw new NotImplementedException();
    }

    public List<FIlme> Listar()
    {
        try
        {
            List<FIlme> ListaFilmes = _context.FIlmes.ToList();
            return ListaFilmes;
        }
        catch (Exception)
        {

            throw;
        }
    }
}
