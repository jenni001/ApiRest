using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiRest.Models;

namespace ApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatosController : ControllerBase
    {
        private readonly DatosContext _context;
        // Se declara el random
        Random random=new Random();

        public DatosController(DatosContext context)
        {
            _context = context;
        }

        // GET: api/Datos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Datos>>> GetDatos()
        {
            foreach (var item in _context.Datos)
            {
                item.Ultimo=random.Next();

                if(item.Ultimo>item.Max){
                    item.Max=item.Ultimo;
                }
            }

            _context.SaveChanges();

            return await _context.Datos.ToListAsync();
        }

        // GET: api/Datos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Datos>> GetDatos(string id)
        {
            var datos = await _context.Datos.FindAsync(id);

            if (datos == null)
            {
                return NotFound();
            }

            

            datos.Ultimo=random.Next();

            if(datos.Ultimo>datos.Max){
                datos.Max=datos.Ultimo;
            }

            await PutDatos(datos.Nombre, datos);

            return datos;

        }

        // PUT: api/Datos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDatos(string id, Datos datos)
        {
            if (id != datos.Nombre)
            {
                return BadRequest();
            }

            datos.Ultimo=random.Next();

            if(datos.Ultimo>datos.Max){
                datos.Max=datos.Ultimo;
            }

            _context.Entry(datos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DatosExists(id))
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

        // POST: api/Datos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Datos>> PostDatos(Datos datos)
        {
            datos.Ultimo=random.Next();

            if(datos.Ultimo>datos.Max){
                datos.Max=datos.Ultimo;
            }   

            _context.Datos.Add(datos);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DatosExists(datos.Nombre))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetDatos), new { id = datos.Nombre }, datos);
        }

        // DELETE: api/Datos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDatos(string id)
        {
            var datos = await _context.Datos.FindAsync(id);
            if (datos == null)
            {
                return NotFound();
            }

            _context.Datos.Remove(datos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DatosExists(string id)
        {
            return _context.Datos.Any(e => e.Nombre == id);
        }
    }
}
