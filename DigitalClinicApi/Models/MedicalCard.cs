using System;
using System.Collections.Generic;

namespace DigitalClinicApi.Models;

public partial class MedicalCard
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public int BloodTypeId { get; set; }

    public int Height { get; set; }

    public decimal Weight { get; set; }

    public string ChronicDisease { get; set; } = null!;

    public string Allergie { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual BloodType BloodType { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;
}
