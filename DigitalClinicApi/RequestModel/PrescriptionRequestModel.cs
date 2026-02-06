namespace DigitalClinicApi.RequestModel
{
    public class PrescriptionRequestModel
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int MedicationId { get; set; }
        public decimal Quantity { get; set; }
        public int DoseUnitId { get; set; }
        public int FrequencyId { get; set; }
        public int DurationInDays { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public int InstructionId { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
