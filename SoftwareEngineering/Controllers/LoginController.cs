using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftwareEngineering.DataObjects;
using SoftwareEngineering.Entities;
using BC = BCrypt.Net.BCrypt;

namespace SoftwareEngineering.Controllers
{
    [Route("api")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private Context context;

        public LoginController(Context context)
        {
            this.context = context;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Kunde>> RegisterKunde([FromBody] KundeRegister kunde)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (await context.Kunden.AnyAsync(k => k.Email == kunde.Email))
                return Conflict(ModelState);

            var entity = kunde.ToEntity();

            await context.AddAsync(entity);
            await context.SaveChangesAsync();

            var returnValue = new Kunde(entity);

            return Ok(returnValue);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Kunde>> LoginKunde([FromBody] LoginData login)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = await context.Kunden.Where(k => k.Email == login.Email).FirstOrDefaultAsync();

            if (entity == null) return NotFound("Es existiert kein Kundenkonto mit dieser E-Mail Adresse!");

            if (!BC.Verify(login.Password, entity.Password_Hash)) return BadRequest("Passwort ist falsch!");

            return Ok(new Kunde(entity));
        }
    }
}
