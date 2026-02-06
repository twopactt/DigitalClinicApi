namespace DigitalClinicApi.RequestModel
{
    public class AdminAuthModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class DoctorAuthModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class PatientAuthModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
