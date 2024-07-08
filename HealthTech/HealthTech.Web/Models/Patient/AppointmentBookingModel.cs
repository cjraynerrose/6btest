namespace HealthTech.Web.Models.Patient
{
    public class AppointmentBookingModel
    {
        public string Name { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Issue { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
    }
}
