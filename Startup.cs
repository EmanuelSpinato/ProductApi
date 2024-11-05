using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Configura o DbContext para usar PostgreSQL com a string de conexão
        services.AddDbContext<ProductDbContext>(options =>
            options.UseNpgsql("Host=localhost;Database=productdb;Username=postgres;Password=123"));

        // Configura o controlador para lidar com ciclos de referência
        services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; // Ignorar ciclos
                options.SerializerSettings.MaxDepth = 5; // Limitar profundidade
            });

        // Adicionar suporte ao Swagger para documentação da API
        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}