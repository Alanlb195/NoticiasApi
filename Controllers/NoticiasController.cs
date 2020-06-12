using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoticiasApi.Database;

namespace NoticiasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoticiasController : ControllerBase
    {
        private readonly NoticiasContext _context;

        public NoticiasController(NoticiasContext context)
        {
            _context = context;
        }

        // GET: api/Noticias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Noticia>>> GetNoticia()
        {
            return await _context.Noticia.Include("Autor").ToListAsync();
        }

        // GET: api/Noticias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Noticia>> GetNoticia(int id)
        {
            var noticia = await _context.Noticia.FindAsync(id);

            if (noticia == null)
            {
                return NotFound();
            }

            return noticia;
        }

        // PUT: api/Noticias/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNoticia(int id, Noticia noticia)
        {
            if (id != noticia.NoticiaId)
            {
                return BadRequest();
            }

            _context.Entry(noticia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoticiaExists(id))
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

        // POST: api/Noticias
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Noticia>> PostNoticia([FromBody] Noticia noticia)
        {
            _context.Noticia.Add(noticia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNoticia", new { id = noticia.NoticiaId }, noticia);
        }

        // DELETE: api/Noticias/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Noticia>> DeleteNoticia(int id)
        {
            var noticia = await _context.Noticia.FindAsync(id);
            if (noticia == null)
            {
                return NotFound();
            }

            _context.Noticia.Remove(noticia);
            await _context.SaveChangesAsync();

            return noticia;
        }

        private bool NoticiaExists(int id)
        {
            return _context.Noticia.Any(e => e.NoticiaId == id);
        }
    }
}
