using DigitalClinicApi.DatabaseContext;
using DigitalClinicApi.Helpers;
using DigitalClinicApi.ResponceModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigitalClinicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientHistoryController : ControllerBase
    {
        private readonly DigitalTwinPatientDbContext _db;
        private readonly JwtService _jwtService;

        public PatientHistoryController(DigitalTwinPatientDbContext db, JwtService jwtService)
        {
            _db = db;
            _jwtService = jwtService;
        }

        [HttpGet("{patientId}")]
        public async Task<IActionResult> GetPatientHistory(int patientId)
        {
            try
            {
                var patient = await _db.Patients
                    .Where(p => p.Id == patientId)
                    .Select(p => new
                    {
                        p.Id,
                        p.Surname,
                        p.Name,
                        p.Patronymic
                    })
                    .FirstOrDefaultAsync();

                if (patient == null)
                    return NotFound(new { error = "Patient not found" });

                var histories = await _db.PatientHistories
                    .Include(ph => ph.Diagnosis)
                    .Include(ph => ph.DiagnosisStatus)
                    .Where(ph => ph.PatientId == patientId)
                    .Select(ph => new PatientHistoryResponseModel
                    {
                        PatientFullName = $"{patient.Surname} {patient.Name} {patient.Patronymic}".Trim(),
                        DiagnosisName = ph.Diagnosis.Name,
                        DiagnosedDate = ph.DiagnosedDate,
                        DiagnosisStatusName = ph.DiagnosisStatus.Name,
                        Notes = ph.Notes
                    })
                    .ToListAsync();

                foreach (var history in histories)
                {
                    var prescription = await _db.Prescriptions
                        .Include(p => p.Medication)
                        .Include(p => p.DoseUnit)
                        .Include(p => p.Frequency)
                        .Where(p => p.PatientId == patientId)
                        .OrderByDescending(p => p.StartDate)
                        .FirstOrDefaultAsync();

                    if (prescription != null)
                    {
                        history.MedicationName = prescription.Medication.Name;
                        history.Quantity = prescription.Quantity;
                        history.DoseUnitName = prescription.DoseUnit.Name;
                        history.FrequencyName = prescription.Frequency.Name;
                        history.DurationInDays = prescription.DurationInDays;
                    }
                }

                var result = new PatientFullHistoryResponseModel
                {
                    PatientId = patientId,
                    PatientFullName = $"{patient.Surname} {patient.Name} {patient.Patronymic}".Trim(),
                    Histories = histories,
                };

                return Ok(result);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
