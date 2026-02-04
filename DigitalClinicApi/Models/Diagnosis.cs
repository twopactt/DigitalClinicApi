using System;
using System.Collections.Generic;

namespace DigitalClinicApi.Models;

public partial class Diagnosis
{
    public int Id { get; set; }

    public string CodeMkb { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int DiagnosisCategoryId { get; set; }

    public virtual DiagnosisCategory DiagnosisCategory { get; set; } = null!;
}
