using FilmesMoura.WebAPI.BdContextFilme;
using FilmesMoura.WebAPI.Interfaces;
using FilmesMoura.WebAPI.Repositories;
using Microsoft.EntityFrameworkCore;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //Adiciona o contexto do banco de dados (exemplo SQL Server)

        builder.Services.AddDbContext<FilmeContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


        //Adiciona o rep. ao contrainer de injecao de dependencia
        builder.Services.AddScoped<IFilmeRepository, FilmesRepository>();
        builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();

        //adiciona servico de controllers
        builder.Services.AddControllers();
        var app = builder.Build();

        //adiciona mapeamneto de controllersA
        app.MapControllers();

        app.Run();
    }
}