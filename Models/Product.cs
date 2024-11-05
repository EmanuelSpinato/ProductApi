using System.ComponentModel.DataAnnotations;

namespace ProductAPI.Models
{
    public class Product
    {
        [Key] // Identificador único
        public int Id { get; set; }  // Id único do produto

        [Required(ErrorMessage = "O nome do produto não pode ser nulo.")]
        [StringLength(100, ErrorMessage = "O nome do produto deve ter no máximo 100 caracteres.")]
        public string Name { get; set; } = string.Empty;  // Nome do produto (não pode ser nulo)

        [Required(ErrorMessage = "A descrição do produto não pode ser nula.")]
        public string Description { get; set; } = string.Empty;  // Descrição do produto

        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que 0,01.")]
        public decimal Price { get; set; }  // Preço (não pode ser menor que 0,01)

        [MaxLength(10, ErrorMessage = "O produto pode ter no máximo 10 imagens.")]
        public List<Asset> Assets { get; set; } = new();  // Lista de imagens e vídeos

        [MaxLength(5, ErrorMessage = "O produto pode ter no máximo 5 vídeos.")]
        public List<ProductAttribute> Attributes { get; set; } = new();  // Atributos dinâmicos

        public List<Category> Categories { get; set; } = new();  // Lista de categorias
    }
}