namespace DigitalClinicApi.Models;

public partial class Consultation
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public int DoctorId { get; set; }

    public DateTime DateConsultation { get; set; }

    public int ConsultationTypeId { get; set; }

    public string? Notes { get; set; }

    public virtual ConsultationType ConsultationType { get; set; } = null!;

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;
}
