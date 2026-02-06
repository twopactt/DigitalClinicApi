using DigitalClinicApi.DatabaseContext;
using DigitalClinicApi.Helpers;
using DigitalClinicApi.Models;
using DigitalClinicApi.RequestModel;
using DigitalClinicApi.ResponceModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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
                Password = PasswordHasher.Hash(model.Password)
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

        [HttpGet("/all_patients")]
        public IActionResult GetAll()
        {
            var patients = _db.Patients.ToList();
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var patient = _db.Patients.FirstOrDefault(p => p.Id == id);
            if (patient == null)
                return NotFound(new { Message = "Пациент не найден" });

            return Ok(patient);
        }

        [HttpPost("/create-patient")]
        public IActionResult Create([FromBody] CreatePatientRequestModel model)
        {
            if (_db.Patients.Any(p => p.Login == model.Login))
                return BadRequest(new { Message = "Логин уже существует" });

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
                Password = PasswordHasher.Hash(model.Password)
            };

            _db.Patients.Add(patient);
            _db.SaveChanges();

            return Ok(patient);
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update(int id, [FromBody] UpdatePatientRequestModel model)
        {
            var patient = _db.Patients.FirstOrDefault(p => p.Id == id);
            if (patient == null)
                return NotFound();

            patient.Surname = model.Surname;
            patient.Name = model.Name;
            patient.Patronymic = model.Patronymic;
            patient.Phone = model.Phone;
            patient.AddressId = model.AddressId;

            _db.SaveChanges();
            return Ok(new PatientResponseModel
            {
                Id = patient.Id,
                Surname = patient.Surname,
                Name = patient.Name,
                Patronymic = patient.Patronymic,
                Email = patient.Email,
                Phone = patient.Phone
            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var patient = _db.Patients.FirstOrDefault(p => p.Id == id);
            if (patient == null)
                return NotFound();

            _db.Patients.Remove(patient);
            _db.SaveChanges();
            return Ok(new { Message = "Пациент удалён" });
        }
    }
}
