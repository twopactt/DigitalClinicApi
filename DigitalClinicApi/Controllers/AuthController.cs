using DigitalClinicApi.DatabaseContext;
using DigitalClinicApi.Helpers;
using DigitalClinicApi.RequestModel;
using DigitalClinicApi.ResponceModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace DigitalClinicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DigitalTwinPatientDbContext _db;
        private readonly JwtService _jwtService;

        public AuthController(DigitalTwinPatientDbContext db, JwtService jwtService)
        {
            _db = db;
            _jwtService = jwtService;
        }

        [HttpPost("admin/login")]
        public IActionResult AdminLogin([FromBody] AdminAuthModel model)
        {
            var hashedPassword = HashPassword(model.Password);

            var admin = _db.Admins
                .FirstOrDefault(a => a.Login == model.Login && a.Password == hashedPassword);

            if (admin == null)
                return Unauthorized(new { Message = "Неверный логин или пароль" });

            var token = _jwtService.GenerateToken(
                userId: admin.Id.ToString(),
                role: "Admin",
                name: $"{admin.Surname} {admin.Name}"
            );

            return Ok(new AdminAuthResponse
            {
                Admin = admin,
                Token = token,
                Role = "Admin"
            });
        }

        [HttpPost("doctor/login")]
        public IActionResult DoctorLogin([FromBody] DoctorAuthModel model)
        {
            var hashedPassword = HashPassword(model.Password);

            var doctor = _db.Doctors
                .FirstOrDefault(d => d.Login == model.Login && d.Password == hashedPassword);

            if (doctor == null)
                return Unauthorized(new { Message = "Неверный логин или пароль" });

            var token = _jwtService.GenerateToken(
                userId: doctor.Id.ToString(),
                role: "Doctor",
                name: $"{doctor.Surname} {doctor.Name}"
            );

            return Ok(new DoctorAuthResponse
            {
                Doctor = doctor,
                Token = token,
                Role = "Doctor"
            });
        }

        [HttpPost("patient/login")]
        public IActionResult PatientLogin([FromBody] PatientAuthModel model)
        {
            var hashedPassword = HashPassword(model.Password);

            var patient = _db.Patients
                .FirstOrDefault(p => p.Login == model.Login && p.Password == hashedPassword);

            if (patient == null)
                return Unauthorized(new { Message = "Неверный логин или пароль" });

            var token = _jwtService.GenerateToken(
                userId: patient.Id.ToString(),
                role: "Patient",
                name: $"{patient.Surname} {patient.Name}"
            );

            return Ok(new PatientAuthResponse
            {
                Patient = patient,
                Token = token,
                Role = "Patient"
            });
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
