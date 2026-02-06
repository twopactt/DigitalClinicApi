namespace DigitalClinicApi.ResponceModels
{
    public class ConsultationResponseModel
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime DateConsultation { get; set; }
        public int ConsultationTypeId { get; set; }
        public string? Notes { get; set; }
    }
}
