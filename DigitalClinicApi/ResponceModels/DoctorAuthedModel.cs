using DigitalClinicApi.Models;

namespace DigitalClinicApi.ResponceModels
{
    public class DoctorAuthedModel
    {
        public Doctor Doctor { get; set; }
        public int Token { get; set; }
    }
}
