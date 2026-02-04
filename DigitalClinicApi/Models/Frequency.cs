using System;
using System.Collections.Generic;

namespace DigitalClinicApi.Models;

public partial class Frequency
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
