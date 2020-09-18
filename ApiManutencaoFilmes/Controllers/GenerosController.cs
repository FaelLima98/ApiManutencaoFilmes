using ApiManutencaoFilmes.Models;
using ApiManutencaoFilmes.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiManutencaoFilmes.Controllers {
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class GenerosController : ControllerBase
    {
        private readonly IDataRepository<Genero> _repo;

        public GenerosController(IDataRepository<Genero> repo)
        {
            _repo = repo;
        }

        // GET: api/Generos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genero>>> GetGeneros()
        {

            try {
                var generos = await _repo.Get().ToListAsync();

                return generos;
                
            } catch (Exception) {

                return BadRequest();
            }
            
        }

        // GET: api/Generos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Genero>> GetGenero(int id) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var genero = await _repo.GetById(p => p.GeneroId == id);

            if (!GeneroExists(id)) {
                return NotFound();
            }

            return genero;
        }

        //// PUT: api/Generos/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for
        //// more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutGenero(int id, Genero genero)
        //{
        //    if (id != genero.GeneroId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(genero).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!GeneroExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Generos
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for
        //// more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPost]
        //public async Task<ActionResult<Genero>> PostGenero(Genero genero)
        //{
        //    _context.Generos.Add(genero);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetGenero", new { id = genero.GeneroId }, genero);
        //}

        //// DELETE: api/Generos/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Genero>> DeleteGenero(int id)
        //{
        //    var genero = await _context.Generos.FindAsync(id);
        //    if (genero == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Generos.Remove(genero);
        //    await _context.SaveChangesAsync();

        //    return genero;
        //}

        private bool GeneroExists(int id) {
            var exists = _repo.GetById(e => e.GeneroId == id).Result == null ? false : true;

            return exists;
        }
    }
}
