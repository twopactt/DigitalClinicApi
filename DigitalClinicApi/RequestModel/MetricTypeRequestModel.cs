namespace DigitalClinicApi.RequestModel
{
    public class MetricTypeRequestModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int UnitOfMetricTypeId { get; set; }
        public decimal? MinValue { get; set; }
        public decimal? MaxValue { get; set; }
    }
}
