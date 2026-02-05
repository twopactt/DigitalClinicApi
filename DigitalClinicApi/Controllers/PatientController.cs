using DigitalClinicApi.DatabaseContext;
using DigitalClinicApi.Helpers;
using DigitalClinicApi.Models;
using DigitalClinicApi.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace DigitalClinicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly DigitalTwinPatientDbContext _db;
        private readonly JwtService _jwtService;

        public PatientController(DigitalTwinPatientDbContext db, JwtService jwtService)
        {
            _db = db;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] RegisterPatientModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Login) || string.IsNullOrWhiteSpace(model.Password))
                return BadRequest(new { Message = "Логин и пароль обязательны" });

            var passwordValidation = PasswordValidator.ValidatePatientPassword(model.Password);
            if (!passwordValidation.IsValid)
                return BadRequest(new { Message = passwordValidation.Message });

            if (_db.Patients.Any(p => p.Login == model.Login))
                return BadRequest(new { Message = "Логин уже существует" });

            if (_db.Patients.Any(p => p.Email == model.Email))
                return BadRequest(new { Message = "Email уже зарегистрирован" });

            if (!_db.Genders.Any(g => g.Id == model.GenderId))
                return BadRequest(new { Message = "Указанный пол не найден" });

            if (!_db.Addresses.Any(a => a.Id == model.AddressId))
                return BadRequest(new { Message = "Указанный адрес не найден" });

            var patient = new Patient
            {
                Surname = model.Surname,
                Name = model.Name,
                Patronymic = model.Patronymic,
                GenderId = model.GenderId,
                AddressId = model.AddressId,
                Email = model.Email,
                Birthday = model.Birthday,
                Phone = model.Phone,
                Login = model.Login,
                Password = HashPassword(model.Password)
            };

            _db.Patients.Add(patient);
            _db.SaveChanges();

            var token = _jwtService.GenerateToken(
                userId: patient.Id.ToString(),
                role: "Patient",
                name: $"{patient.Surname} {patient.Name}"
            );

            return Ok(new
            {
                Message = "Регистрация успешна",
                Patient = new
                {
                    patient.Id,
                    patient.Surname,
                    patient.Name,
                    patient.Email,
                    patient.Phone,
                    patient.Login
                },
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
