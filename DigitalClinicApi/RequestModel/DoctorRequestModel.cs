namespace DigitalClinicApi.RequestModel
{
    public class CreateDoctorRequestModel
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

    public class UpdateDoctorRequestModel
    {
        public string Phone { get; set; }
        public int DepartmentId { get; set; }
    }
}
