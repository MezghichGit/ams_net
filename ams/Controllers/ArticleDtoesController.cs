using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ams.Dtos;
using ams.Models;
using AutoMapper;

namespace ams.Controllers
{
    public class ArticleProviderDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ProviderName { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class ArticleDtoesController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public ArticleDtoesController(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ArticleDtoes
        [HttpGet]
        /*
        public async Task<ActionResult<IEnumerable<ProviderDto>>> GetArticleDto()
        {
            var dataProviders = (from a in await _context.Articles.ToListAsync()

                                 select new ArticleDto
                                 {
                                     Id = a.Id,
                                     Name = a.Name,
                                     Price = a.Price,
                                     ProviderId = a.ProviderId
                                 }).ToList();


            return Ok(dataProviders);
        }*/
        public async Task<ActionResult<IEnumerable<ArticleProviderDto>>> GetArticles()
        {
            /*var articles = (from a in await _context.Articles.ToListAsync()

                            select new Article
                            {
                                Id = a.Id,
                                Name = a.Name,
                                Price = a.Price,
                                Provider = a.Provider,
                                //ProviderAddress = "bb"

                            }).ToList();


            return Ok(articles);*/
            var dataArticleProvider = (from a in await _context.Articles.ToListAsync()
                                       join b in await _context.Providers.ToListAsync() on a.ProviderId equals b.Id

                                       select new ArticleProviderDto
                                       {
                                           Id = a.Id,
                                           Name = a.Name,
                                           Price = a.Price,
                                           ProviderName = b.Name

                                       }).ToList();


            return Ok(dataArticleProvider);


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

            var newArticle = _mapper.Map<Article>(articleDto);
            _context.Articles.Add(newArticle);
            await _context.SaveChangesAsync();

            articleDto.Id = newArticle.Id;

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
