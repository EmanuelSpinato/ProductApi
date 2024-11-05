using Microsoft.AspNetCore.Mvc; // Para ApiController, ControllerBase, HttpPost, HttpGet
using Microsoft.EntityFrameworkCore; // Para ApplicationDbContext e métodos relacionados ao EF
using ProductAPI.Models;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ProductDbContext _context;

    public ProductController(ProductDbContext context)
    {
        _context = context;
    }

    // Endpoint para criar um novo produto
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] Product product)
    {
        // Verifica se o modelo é válido
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Verifica se já existe um produto com o mesmo ID
        if (await _context.Products.AnyAsync(p => p.Id == product.Id))
            return Conflict("Um produto com este ID já existe.");

        // Verifica o número de imagens
        if (product.Assets.Count > 10)
            return BadRequest("O produto pode ter no máximo 10 imagens.");

        // Verifica o número de vídeos
        int videoCount = product.Assets.Count(a => a.Type == "video");
        if (videoCount > 5)
            return BadRequest("O produto pode ter no máximo 5 vídeos.");

        // Verifica se o preço é válido
        if (product.Price < 0.01m)
            return BadRequest("O preço do produto não pode ser menor que 0,01.");

        // Verifica se o produto possui pelo menos uma categoria
        if (!product.Categories.Any())
            return BadRequest("O produto deve ter pelo menos uma categoria.");

        // Adiciona o produto ao contexto
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        // Retorna a resposta de criação com a localização do novo produto
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    // Endpoint para obter um produto pelo ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var product = await _context.Products
            .Include(p => p.Categories)
            .Include(p => p.Assets)
            .Include(p => p.Attributes)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
            return NotFound();

        return Ok(product);
    }
}
