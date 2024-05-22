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
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderDtoesController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public ProviderDtoesController(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


    // GET: api/ProviderDtoes
    [HttpGet]
        public async Task<ActionResult<IEnumerable<ProviderDto>>> GetProviderDto()
        {
            return await _context.ProviderDto.ToListAsync();
        }

        // GET: api/ProviderDtoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Provider>> GetProviderDto(int id)
        {
            var provider = await _context.Providers.FindAsync(id);

            if (provider == null)
            {
                return NotFound();
            }

            return provider;
        }

        // PUT: api/ProviderDtoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProviderDto(int id, ProviderDto providerDto)
        {
            if (id != providerDto.Id)
            {
                return BadRequest();
            }

            _context.Entry(providerDto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProviderDtoExists(id))
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

        // POST: api/ProviderDtoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProviderDto>> PostProviderDto(ProviderDto providerDto)
        {
            // _context.ProviderDto.Add(providerDto);
            //await _context.SaveChangesAsync();

            var newProvider = _mapper.Map<Provider>(providerDto);
            _context.Providers.Add(newProvider);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProviderDto", new { id = newProvider.Id }, providerDto);
        }

        // DELETE: api/ProviderDtoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProviderDto(int id)
        {
            var providerDto = await _context.ProviderDto.FindAsync(id);
            if (providerDto == null)
            {
                return NotFound();
            }

            _context.ProviderDto.Remove(providerDto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProviderDtoExists(int id)
        {
            return _context.ProviderDto.Any(e => e.Id == id);
        }
    }
}
