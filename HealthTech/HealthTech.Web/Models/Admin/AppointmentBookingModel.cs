using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HealthTech.Web.Models.Admin
{
    public class AppointmentAdminModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Appointment Date & Time")]
        public DateTimeOffset Date { get; set; }

        [Required]
        [DataType(DataType.MultilineText)] // TODO : doesnt seem to be respected in the view
        [Display(Name = "Please describe your issue")]
        public string Issue { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Display(Name = "Approved")]
        public bool Approved { get; set; }
    }
}
