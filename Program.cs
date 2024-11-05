using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductAPI.Models; // Certifique-se de que o namespace esteja correto

var builder = WebApplication.CreateBuilder(args);

// Limpa provedores de logging padrão e adiciona o console
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Configura o DbContext para usar PostgreSQL
builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adicionar controladores como serviços
builder.Services.AddControllers();

// Adicionar Swagger para documentação da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar o pipeline HTTP para o ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();