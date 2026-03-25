using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Repositories;
using EventPlus.WebAPI.Repositorios;
using EventPlus.WebAPI.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using System.Security.Claims;
using Azure.AI.ContentSafety;
using EventPlus.WebAPI.Repositories;
using EventPlus.WebApi.Repositories;

var builder = WebApplication.CreateBuilder(args);


//Configurar o contexto do banco de dados

builder.Services.AddDbContext<EventContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//2. Registrar as repositories (injecao de dependencia)
builder.Services.AddScoped <ITipoEventoRepository, TipoEventoRepository>();
builder.Services.AddScoped <ITipoUsuarioRepository, TipoUsuarioRepository>();
builder.Services.AddScoped <IInstituicaoRepository, InstituicaoRepository>();
builder.Services.AddScoped <IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped <IEventoRepository, EventoRepository>();
builder.Services.AddScoped <IPresencaRepository, PresencaRepository>();
builder.Services.AddScoped<IComentarioEventoRepository, ComentarioEventoRepository>();

////Configurando o azure content safety
//var endpoint = "https://azure.com";
//var apikey = "";

//var client = new ContentSafetyClient(new Uri(endpoint), new Azure.AzureKeyCredential(apikey));
//builder.Services.AddSingleton(client);

//adiciona o Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(Options =>
{
    Options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API de Eventos",
        Description = "API para gerenciamento de eventos",
        TermsOfService = new Uri("https://exemplo"),
        Contact = new OpenApiContact
        {
            Name = "Giulia",
            Url = new Uri("https://exemplo")
        },
        License = new OpenApiLicense
        {
            Name = "Licensa de exemplo",
            Url = new Uri("https://exemplo")
        }
    });
    Options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Bota o token ai j�o E TEM Q SER JWT:"
    });
    Options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = Array.Empty<string>().ToList()
    });
});


builder.Services.AddControllers();

builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = "JwtBearer";
    options.DefaultAuthenticateScheme = "JwtBearer";
})
    .AddJwtBearer("JwtBearer", options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,// Valida o emissor do token
            ValidateAudience = true, // Valida o destinatário do token
            ValidateLifetime = true, // Valida se o token expirou
            ValidateIssuerSigningKey = true, // Valida a chave de assinatura do token
            ValidIssuer = "api_events", // Emissor do token
            ValidAudience = "api_events", // Destinatário do token
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("Filmes-Chave-Autenticacao-webapi-dev")),// Chave de assinatura do token
            ClockSkew = TimeSpan.Zero // Elimina a tolerância de expiração do token

        };
    });

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger(options => { });
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Eventos v1");
        options.RoutePrefix = string.Empty;
    });
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();