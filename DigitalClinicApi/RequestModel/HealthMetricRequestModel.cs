namespace DigitalClinicApi.RequestModel
{
    public class HealthMetricRequestModel
    {
        public int PatientId { get; set; }
        public int MetricTypeId { get; set; }
        public decimal Value { get; set; }
        public DateTime MeasuredAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
