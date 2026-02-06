namespace DigitalClinicApi.ResponceModels
{
    public class PatientResponseModel
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string? Patronymic { get; set; }
        public string Email { get; set; }
        public DateOnly Birthday { get; set; }
        public string Phone { get; set; }
    }
}
