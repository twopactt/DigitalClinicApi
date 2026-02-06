using DigitalClinicApi.Models;

namespace DigitalClinicApi.ResponceModels
{
    public class AdminAuthResponse
    {
        public Admin Admin { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
    }

    public class DoctorAuthResponse
    {
        public Doctor Doctor { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
    }

    public class PatientAuthResponse
    {
        public Patient Patient { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
    }
}
