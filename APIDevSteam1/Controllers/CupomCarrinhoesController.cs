using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIDevSteam1.Data;
using APIDevSteam1.Models;

namespace APIDevSteam1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CupomCarrinhoesController : ControllerBase
    {
        private readonly APIContext _context;

        public CupomCarrinhoesController(APIContext context)
        {
            _context = context;
        }

        // GET: api/CupomCarrinhoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CupomCarrinho>>> GetCuponsCarrinhos()
        {
            return await _context.CuponsCarrinhos.ToListAsync();
        }

        // GET: api/CupomCarrinhoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CupomCarrinho>> GetCupomCarrinho(Guid id)
        {
            var cupomCarrinho = await _context.CuponsCarrinhos.FindAsync(id);

            if (cupomCarrinho == null)
            {
                return NotFound();
            }

            return cupomCarrinho;
        }

        // PUT: api/CupomCarrinhoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCupomCarrinho(Guid id, CupomCarrinho cupomCarrinho)
        {
            if (id != cupomCarrinho.CupomCarrinhoId)
            {
                return BadRequest();
            }

            _context.Entry(cupomCarrinho).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CupomCarrinhoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CupomCarrinhoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CupomCarrinho>> PostCupomCarrinho(CupomCarrinho cupomCarrinho)
        {
            _context.CuponsCarrinhos.Add(cupomCarrinho);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCupomCarrinho", new { id = cupomCarrinho.CupomCarrinhoId }, cupomCarrinho);
        }

        // DELETE: api/CupomCarrinhoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCupomCarrinho(Guid id)
        {
            var cupomCarrinho = await _context.CuponsCarrinhos.FindAsync(id);
            if (cupomCarrinho == null)
            {
                return NotFound();
            }

            _context.CuponsCarrinhos.Remove(cupomCarrinho);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CupomCarrinhoExists(Guid id)
        {
            return _context.CuponsCarrinhos.Any(e => e.CupomCarrinhoId == id);
        }
        [HttpPost]
        [Route("AplicarCupom")]
        public async Task<IActionResult> AplicarCupom(Guid carrinhoId, Guid cupomId)
        {
            // Verifica se o carrinho existe
            var carrinho = await _context.Carinhos.FindAsync(carrinhoId);
            if (carrinho == null)
            {
                return NotFound("Carrinho não encontrado.");
            }

            // Verifica se o cupom existe
            var cupom = await _context.Cupons.FindAsync(cupomId);
            if (cupom == null)
            {
                return NotFound("Cupom não encontrado.");
            }

            // Verifica se o cupom é válido e está ativo
            if (!cupom.Ativo.HasValue || !cupom.Ativo.Value || (cupom.DataValidade.HasValue && cupom.DataValidade.Value < DateTime.UtcNow))
            {
                return BadRequest("Cupom inválido ou expirado.");
            }

            // Calcula o desconto
            var desconto = (carrinho.ValorTotal * cupom.Desconto) / 100;
            carrinho.ValorTotal -= desconto;

            // Atualiza o carrinho no banco de dados
            _context.Entry(carrinho).State = EntityState.Modified;

            // Cria a relação entre o cupom e o carrinho
            var cupomCarrinho = new CupomCarrinho
            {
                CupomCarrinhoId = Guid.NewGuid(),
                CarrinhoId = carrinhoId,
                CupomId = cupomId
            };
            _context.CuponsCarrinhos.Add(cupomCarrinho);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao salvar as alterações no banco de dados.");
            }

            return Ok(new
            {
                Mensagem = "Cupom aplicado com sucesso.",
                CarrinhoId = carrinhoId,
                ValorTotalAtualizado = carrinho.ValorTotal
            });
        }
    }
}
