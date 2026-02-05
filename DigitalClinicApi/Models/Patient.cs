namespace DigitalClinicApi.Models;

public partial class Patient
{
    public int Id { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Patronymic { get; set; }

    public int GenderId { get; set; }

    public int AddressId { get; set; }

    public string Email { get; set; } = null!;

    public DateOnly Birthday { get; set; }

    public string Phone { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<Consultation> Consultations { get; set; } = new List<Consultation>();

    public virtual Gender Gender { get; set; } = null!;

    public virtual ICollection<HealthMetric> HealthMetrics { get; set; } = new List<HealthMetric>();

    public virtual ICollection<MedicalCard> MedicalCards { get; set; } = new List<MedicalCard>();

    public virtual ICollection<PatientComplaint> PatientComplaints { get; set; } = new List<PatientComplaint>();

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();

    public virtual ICollection<WellnessJournal> WellnessJournals { get; set; } = new List<WellnessJournal>();
}
