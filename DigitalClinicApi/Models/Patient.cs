using System;
using System.Collections.Generic;

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

    public virtual Gender Gender { get; set; } = null!;

    public virtual ICollection<PatientComplaint> PatientComplaints { get; set; } = new List<PatientComplaint>();

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
