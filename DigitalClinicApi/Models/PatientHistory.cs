using System;
using System.Collections.Generic;

namespace DigitalClinicApi.Models;

public partial class PatientHistory
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public int DiagnosisId { get; set; }

    public DateOnly DiagnosedDate { get; set; }

    public int DiagnosisStatusId { get; set; }

    public string? Notes { get; set; }

    public virtual Diagnosis Diagnosis { get; set; } = null!;

    public virtual Patient DiagnosisNavigation { get; set; } = null!;

    public virtual DiagnosisStatus DiagnosisStatus { get; set; } = null!;
}
