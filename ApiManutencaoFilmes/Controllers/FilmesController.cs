using ApiManutencaoFilmes.Context;
using ApiManutencaoFilmes.DTOs;
using ApiManutencaoFilmes.Models;
using ApiManutencaoFilmes.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiManutencaoFilmes.Controllers {
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class FilmesController : ControllerBase {

        private readonly IDataRepository<Filme> _repo;
        private readonly IMapper _mapper;

        public FilmesController(IDataRepository<Filme> repo, IMapper mapper) {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: api/Filmes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilmeDTO>>> GetFilmes() {

            try {

                var filmes = await _repo.Get().ToListAsync();
                var filmesDTO = _mapper.Map<List<FilmeDTO>>(filmes);

                return filmesDTO;

            } catch (Exception) {

                return BadRequest();
            }                       
        }

        // GET: api/Filmes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FilmeDTO>> GetFilme([FromRoute] int id) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var filme = await _repo.GetById(p => p.FilmeId == id);

            if (!FilmesExists(id)) {
                return NotFound();
            }

            var filmeDTO = _mapper.Map<FilmeDTO>(filme);        

            return filmeDTO;
        }

        // PUT: api/Filmes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<FilmeDTO>> PutFilme([FromRoute] int id, [FromBody] FilmeDTO filmeDto) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            if (id != filmeDto.FilmeId) {
                return BadRequest();
            }

            try {
                var filme = _mapper.Map<Filme>(filmeDto);

                _repo.Update(filme);
                var save = await _repo.SaveAsync(filme);

            } catch (Exception) {
                if (!FilmesExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/Filmes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<FilmeDTO>> PostFilme([FromBody] FilmeDTO filmeDto) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var filme = _mapper.Map<Filme>(filmeDto);

            _repo.Add(filme);

            var save = await _repo.SaveAsync(filme);

            var filmeDTO = _mapper.Map<FilmeDTO>(filme);

            return CreatedAtAction("GetFilme", new { id = filmeDTO.FilmeId }, filmeDTO);
        }

        // DELETE: api/Filmes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FilmeDTO>> DeleteFilme(int id) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var filme = await _repo.GetById(p => p.FilmeId == id);

            if (!FilmesExists(id)) {
                return NotFound();
            }

            _repo.Delete(filme);
            var save = await _repo.SaveAsync(filme);

            var filmeDTO = _mapper.Map<FilmeDTO>(filme);

            return Ok(filmeDTO);
        }

        private bool FilmesExists(int id) {          

            var exists = _repo.GetById(e => e.FilmeId == id).Result == null ? false : true;

            return exists;            
        }
    }
}
