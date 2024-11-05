public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Category? ParentCategory { get; set; }  // Categoria pai para hierarquia
    public List<Category> SubCategories { get; set; } = new();
}