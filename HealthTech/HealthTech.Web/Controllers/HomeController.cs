using HealthTech.Web.Data;
using HealthTech.Web.Models;
using HealthTech.Web.Models.Patient;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HealthTech.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAppointmentRepository _appointmentRepository;

        public HomeController(
            ILogger<HomeController> logger
            ,IAppointmentRepository appointmentRepository)
        {
            _logger = logger;
            _appointmentRepository = appointmentRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult CreateAppointment([FromForm] AppointmentBookingModel model)
        {
            if (ModelState.IsValid)
            {
                var appointment = new Appointment
                {
                    Name = model.Name,
                    Date = model.Date,
                    Issue = model.Issue,
                    ContactNumber = model.ContactNumber,
                    Email = model.Email,
                };

                _appointmentRepository.Create(appointment);

                ViewData["Success"] = true;
                ViewData["Message"] = "Your appointment has been successfully booked.";
            }
            else
            {
                ViewData["Success"] = false;
                ViewData["Message"] = "There was an error with your submission. Please try again.";
            }

            // TODO: Could return a check the status of your appt here
            return View(nameof(Index));
        }
    }
}
