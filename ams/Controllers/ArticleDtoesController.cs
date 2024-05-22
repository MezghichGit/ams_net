using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ams.Dtos;
using ams.Models;

namespace ams.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleDtoesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public ArticleDtoesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/ArticleDtoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticleDto()
        {
            return await _context.Articles.ToListAsync();
        }

        // GET: api/ArticleDtoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleDto>> GetArticleDto(int id)
        {
            var articleDto = await _context.ArticleDto.FindAsync(id);

            if (articleDto == null)
            {
                return NotFound();
            }

            return articleDto;
        }

        // PUT: api/ArticleDtoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticleDto(int id, ArticleDto articleDto)
        {
            if (id != articleDto.Id)
            {
                return BadRequest();
            }

            _context.Entry(articleDto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleDtoExists(id))
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

        // POST: api/ArticleDtoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ArticleDto>> PostArticleDto(ArticleDto articleDto)
        {
            _context.ArticleDto.Add(articleDto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArticleDto", new { id = articleDto.Id }, articleDto);
        }

        // DELETE: api/ArticleDtoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticleDto(int id)
        {
            var articleDto = await _context.ArticleDto.FindAsync(id);
            if (articleDto == null)
            {
                return NotFound();
            }

            _context.ArticleDto.Remove(articleDto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArticleDtoExists(int id)
        {
            return _context.ArticleDto.Any(e => e.Id == id);
        }
    }
}
