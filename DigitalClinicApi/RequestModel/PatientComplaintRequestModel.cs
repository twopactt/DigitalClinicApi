namespace DigitalClinicApi.RequestModel
{
    public class PatientComplaintRequestModel
    {
        public int PatientId { get; set; }
        public int SymptomId { get; set; }
        public DateOnly ComplaintDate { get; set; }
        public int SeverityId { get; set; }
        public string? Description { get; set; }
    }
}
