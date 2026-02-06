using DigitalClinicApi.DatabaseContext;
using DigitalClinicApi.Helpers;
using DigitalClinicApi.Models;
using DigitalClinicApi.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalClinicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly DigitalTwinPatientDbContext _db;
        private readonly JwtService _jwtService;

        public AdminController(DigitalTwinPatientDbContext db, JwtService jwtService)
        {
            _db = db;
            _jwtService = jwtService;
        }

        [HttpPost("create-admin")]
        public IActionResult CreateAdmin([FromBody] CreateAdminModel model)
        {
            if (_db.Admins.Any(a => a.Login == model.Login))
                return BadRequest(new { Message = "Логин уже существует" });

            if (_db.Admins.Any(a => a.Email == model.Email))
                return BadRequest(new { Message = "Email уже зарегистрирован" });

            var passwordValidation = PasswordValidator.ValidateAdminPassword(model.Password);
            if (!passwordValidation.IsValid)
                return BadRequest(new { Message = passwordValidation.Message });

            var admin = new Admin
            {
                Surname = model.Surname,
                Name = model.Name,
                Patronymic = model.Patronymic,
                Email = model.Email,
                Phone = model.Phone,
                Login = model.Login,
                Password = PasswordHasher.Hash(model.Password)
            };

            _db.Admins.Add(admin);
            _db.SaveChanges();

            return Ok(new { Message = "Администратор успешно создан", AdminId = admin.Id });
        }

        [HttpPost("create-doctor")]
        public IActionResult CreateDoctor([FromBody] CreateDoctorModel model)
        {
            if (_db.Doctors.Any(d => d.Login == model.Login))
                return BadRequest(new { Message = "Логин уже существует" });

            if (_db.Doctors.Any(d => d.Email == model.Email))
                return BadRequest(new { Message = "Email уже зарегистрирован" });

            if (!_db.Specializations.Any(s => s.Id == model.SpecializationId))
                return BadRequest(new { Message = "Специализация не найдена" });

            if (!_db.Departments.Any(d => d.Id == model.DepartmentId))
                return BadRequest(new { Message = "Отделение не найдено" });

            var doctor = new Doctor
            {
                Surname = model.Surname,
                Name = model.Name,
                Patronymic = model.Patronymic,
                SpecializationId = model.SpecializationId,
                DepartmentId = model.DepartmentId,
                Email = model.Email,
                Phone = model.Phone,
                Login = model.Login,
                Password = PasswordHasher.Hash(model.Password)
            };

            _db.Doctors.Add(doctor);
            _db.SaveChanges();

            return Ok(new
            {
                Message = "Доктор успешно создан",
                DoctorId = doctor.Id,
                Login = doctor.Login
            });
        }

        [HttpPost("register-patient")]
        public IActionResult RegisterPatient([FromBody] RegisterPatientModel model)
        {
            if (_db.Patients.Any(p => p.Login == model.Login))
                return BadRequest(new { Message = "Логин уже существует" });

            if (_db.Patients.Any(p => p.Email == model.Email))
                return BadRequest(new { Message = "Email уже зарегистрирован" });

            if (!_db.Genders.Any(g => g.Id == model.GenderId))
                return BadRequest(new { Message = "Пол не найден" });

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

            return Ok(new
            {
                Message = "Пациент успешно зарегистрирован",
                PatientId = patient.Id,
                Login = patient.Login
            });
        }

        [HttpGet("all-users")]
        public IActionResult GetAllUsers()
        {
            var admins = _db.Admins.Select(a => new {
                a.Id,
                a.Surname,
                a.Name,
                a.Login,
                a.Email,
                Role = "Admin"
            });

            var doctors = _db.Doctors.Select(d => new {
                d.Id,
                d.Surname,
                d.Name,
                d.Login,
                d.Email,
                Role = "Doctor"
            });

            var patients = _db.Patients.Select(p => new {
                p.Id,
                p.Surname,
                p.Name,
                p.Login,
                p.Email,
                Role = "Patient"
            });

            return Ok(new
            {
                Admins = admins,
                Doctors = doctors,
                Patients = patients
            });
        }

        [HttpGet("statistics")]
        public IActionResult GetStatistics()
        {
            var stats = new
            {
                TotalAdmins = _db.Admins.Count(),
                TotalDoctors = _db.Doctors.Count(),
                TotalPatients = _db.Patients.Count(),
                TotalDepartments = _db.Departments.Count(),
                TotalSpecializations = _db.Specializations.Count(),
                RecentPatients = _db.Patients
                    .OrderByDescending(p => p.Id)
                    .Take(5)
                    .Select(p => new { p.Id, p.Surname, p.Name, p.Email })
            };

            return Ok(stats);
        }
    }
}
