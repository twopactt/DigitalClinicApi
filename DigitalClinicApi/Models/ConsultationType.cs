using System;
using System.Collections.Generic;

namespace DigitalClinicApi.Models;

public partial class ConsultationType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Consultation> Consultations { get; set; } = new List<Consultation>();
}
