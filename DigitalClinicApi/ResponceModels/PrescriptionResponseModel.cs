namespace DigitalClinicApi.ResponceModels
{
    public class PrescriptionResponseModel
    {
        public int Id { get; set; }
        public string Medication { get; set; }
        public decimal Quantity { get; set; }
        public string Status { get; set; }
    }
}
