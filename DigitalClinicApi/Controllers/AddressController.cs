using Microsoft.AspNetCore.Mvc;
using DigitalClinicApi.DatabaseContext;

namespace DigitalClinicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly DigitalTwinPatientDbContext _db;

        public AddressController(DigitalTwinPatientDbContext db)
        {
            _db = db;
        }

        [HttpGet("/all")]
        public IActionResult GetAllAddresses()
        {
            try
            {
                return Ok(_db.Addresses.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
