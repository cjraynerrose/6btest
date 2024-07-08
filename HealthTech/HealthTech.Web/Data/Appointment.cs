using System.ComponentModel.DataAnnotations;

namespace HealthTech.Web.Data
{
    public class Appointment
    {
        public Appointment()
        {
            CreatedAt = DateTimeOffset.Now;
            UpdatedAt = DateTimeOffset.Now;
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Issue { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }

        public bool Approved { get; set; }

        public DateTimeOffset CreatedAt { get; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
