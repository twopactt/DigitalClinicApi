using System;
using System.Collections.Generic;

namespace DigitalClinicApi.Models;

public partial class DiagnosisCategory
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>();
}
