using System;
using System.Collections.Generic;

namespace DigitalClinicApi.Models;

public partial class BloodType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<MedicalCard> MedicalCards { get; set; } = new List<MedicalCard>();
}
