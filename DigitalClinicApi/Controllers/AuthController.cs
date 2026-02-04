using DigitalClinicApi.DatabaseContext;
using DigitalClinicApi.RequestModel;
using DigitalClinicApi.ResponceModels;
using Microsoft.AspNetCore.Mvc;

namespace DigitalClinicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DigitalTwinPatientDbContext _db;

        public AuthController(DigitalTwinPatientDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public IActionResult Auth([FromBody] DoctorAuthModel doctorAuthModel)
        {
#warning Обрати внимание на то, что пароль в БД в хеше
#warning а значит как получишь его на клиенте, перед отправкой в апи, надо его так же захешировать
            var doctor = _db.Doctors
                .Where(d => d.Login == doctorAuthModel.Login
                       && d.Password == doctorAuthModel.Password)
                .FirstOrDefault();
            if (doctor == null)
                return NotFound(null);

            // Тут короче генеришь JWT и отправляешь его обратно
            // Важно так же работать с JWT при следующих запросах к APi
            int token = ($"{doctor.Surname}{doctor.Name}{doctor.Email}").GetHashCode();

            return Ok(new DoctorAuthedModel
            {
                Doctor = doctor,
                Token = token
            });
        }
    }
}
