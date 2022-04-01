using Microsoft.AspNetCore.Mvc;
using PruebaToka.Models;
using PruebaToka.Servicios;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PruebaToka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasFisicasController : ControllerBase
    {
        private readonly IRepositorioPersonasFisicas repositorioPersonasFisicas;

        public PersonasFisicasController(IRepositorioPersonasFisicas repositorioPersonasFisicas)
        {
            this.repositorioPersonasFisicas = repositorioPersonasFisicas;
        }

        // GET: api/<PersonasFisicasController>
        [HttpGet]
        public async Task<IEnumerable<PersonaFisica>> Get()
        {
            return await repositorioPersonasFisicas.Obtener();
        }

        // GET api/<PersonasFisicasController>/5
        [HttpGet("{id}")]
        public async Task<PersonaFisica> Get(int id)
        {
            return await repositorioPersonasFisicas.ObtenerPorId(id);
        }

        // POST api/<PersonasFisicasController>
        [HttpPost]
        public async Task Post([FromBody] PersonaFisica personaFisica)
        {
            if (ModelState.IsValid)
            {
                await repositorioPersonasFisicas.Crear(personaFisica);
            } 
            
        }

        // PUT api/<PersonasFisicasController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] PersonaFisica personaFisica)
        {
            personaFisica.IdPersonaFisica = id;

            if (ModelState.IsValid)
            {
                await repositorioPersonasFisicas.Actualizar(personaFisica);
            }

        }

        // DELETE api/<PersonasFisicasController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await repositorioPersonasFisicas.Borrar(id);
        }
    }
}
