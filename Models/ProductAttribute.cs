public class ProductAttribute
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;  // Nome do atributo
    public string Type { get; set; } = string.Empty;  // Tipo (texto ou numérico)
    public string Value { get; set; } = string.Empty;  // Valor do atributo
}