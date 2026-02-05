namespace DigitalClinicApi.RequestModel
{
    public class CreateAdminModel
    {
        public string Surname { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Patronymic { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class CreateDoctorModel
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string? Patronymic { get; set; }
        public int SpecializationId { get; set; }
        public int DepartmentId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class RegisterPatientModel
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string? Patronymic { get; set; }
        public int GenderId { get; set; }
        public int AddressId { get; set; }
        public string Email { get; set; }
        public DateOnly Birthday { get; set; }
        public string Phone { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
