using BackEndCrud.Data;
using BackEndCrud.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarjetaCreditoController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public TarjetaCreditoController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TarjetaCreditdo>>> GetTarjetas()
        {
            return await _db.TarjetaCredito.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TarjetaCreditdo>> GetTarjeta(int id)
        {
            var dato = await _db.TarjetaCredito.FindAsync(id);

            if(dato == null)
            {
                return NotFound();
            }

            return dato;
        }

        [HttpPost]
        public async Task<ActionResult<TarjetaCreditdo>> PostTarjeta(TarjetaCreditdo tarjeta)
        {
            _db.TarjetaCredito.Add(tarjeta);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTarjeta), new { id = tarjeta.id }, tarjeta);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TarjetaCreditdo>> PutTarjeta (int id, TarjetaCreditdo tarjeta)
        {
            if(id != tarjeta.id)
            {
                return BadRequest();
            }
            _db.Entry(tarjeta).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TarjetaCreditdo>> DeleteTarjeta(int id)
        {
            var dato = await _db.TarjetaCredito.FindAsync(id);
            if(dato == null)
            {
                return NotFound();
            }
            _db.TarjetaCredito.Remove(dato);
            await _db.SaveChangesAsync();

            return NoContent(); 

        }
    }
}
