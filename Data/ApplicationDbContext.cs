using Microsoft.EntityFrameworkCore;
using ProductAPI.Models;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; } // Conjunto de produtos

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Products"); // Nome da tabela

            entity.HasKey(e => e.Id); // Chave primária

            entity.Property(e => e.Name)
                .IsRequired() // Nome é obrigatório
                .HasMaxLength(100); // Comprimento máximo

            entity.Property(e => e.Description)
                .HasMaxLength(500); // Comprimento máximo para a descrição

            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 2)"); // Especificando tipo de coluna
        });
    }
}