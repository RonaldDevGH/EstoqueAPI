using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EstoqueAPI.Data;
using EstoqueAPI.Models;

namespace EstoqueAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/produtos (Lista todos)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            return await _context.produtos.ToListAsync();
        }

        // POST: api/produtos (Cadastra um novo)
        [HttpPost]
        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
        {
            _context.produtos.Add(produto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProdutos), new { id = produto.Id }, produto);
        }

        [HttpPut]
        public async Task<IActionResult> PutEstoque(List<Produto> produtosAtualizados)
        {
            foreach (var p in produtosAtualizados)
            {
                _context.Entry(p).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Produto>> GetProdutoById(int id)
        {
            var produto = await _context.produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            return produto;
        }
    }
}