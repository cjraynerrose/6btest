using HealthTech.Web.Data;
using HealthTech.Web.Models.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthTech.Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AdminController(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        // GET: Admin
        public async Task<IActionResult> Index()
        {
            var appointments = await _appointmentRepository.GetMany();
            List<AppointmentAdminModel> model = appointments.Select(a => new AppointmentAdminModel
            {
                Id = a.Id,
                Name = a.Name,
                Date = a.Date,
                Issue = a.Issue,
                ContactNumber = a.ContactNumber,
                Email = a.Email,
                Approved = a.Approved
            }).ToList();

            return View(model);
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _appointmentRepository.Get(id.Value);
            if (appointment == null)
            {
                return NotFound();
            }

            var model = new AppointmentAdminModel
            {
                Id = appointment.Id,
                Name = appointment.Name,
                Date = appointment.Date,
                Issue = appointment.Issue,
                ContactNumber = appointment.ContactNumber,
                Email = appointment.Email,
                Approved = appointment.Approved
            };

            return View(model);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Date,Issue,ContactNumber,Email,Approved")] AppointmentAdminModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Invalid model state");
            }

            Appointment appointment = new()
            {
                Name = model.Name,
                Date = model.Date,
                Issue = model.Issue,
                ContactNumber = model.ContactNumber,
                Email = model.Email,
                Approved = model.Approved,
                UpdatedAt = DateTime.Now
            };

            await _appointmentRepository.Create(appointment);

            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _appointmentRepository.Get(id.Value);
            if (appointment == null)
            {
                return NotFound();
            }

            var model = new AppointmentAdminModel
            {
                Id = appointment.Id,
                Name = appointment.Name,
                Date = appointment.Date,
                Issue = appointment.Issue,
                ContactNumber = appointment.ContactNumber,
                Email = appointment.Email,
                Approved = appointment.Approved
            };

            return View(model);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Date,Issue,ContactNumber,Email,Approved")] AppointmentAdminModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Appointment appointment = await _appointmentRepository.Get(id);

                    appointment.Name = model.Name;
                    appointment.Date = model.Date;
                    appointment.Issue = model.Issue;
                    appointment.ContactNumber = model.ContactNumber;
                    appointment.Email = model.Email;
                    appointment.Approved = model.Approved;
                    appointment.UpdatedAt = DateTime.Now;

                    await _appointmentRepository.Update(appointment);
                }
                catch (Exception e)
                {
                    // TODO
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpPost, ActionName("ApproveAppointment")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveAppointment(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                Appointment appointment = await _appointmentRepository.Get(id);
                appointment.Approved = !appointment.Approved;
                await _appointmentRepository.Update(appointment);
            }
            catch (Exception ex)
            {
                // TODO replace with frieldly message
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _appointmentRepository.Get(id.Value);

            if (appointment == null)
            {
                return NotFound();
            }

            AppointmentAdminModel model = new()
            {
                Id = appointment.Id,
                Name = appointment.Name,
                Date = appointment.Date,
                Issue = appointment.Issue,
                ContactNumber = appointment.ContactNumber,
                Email = appointment.Email,
                Approved = appointment.Approved
            };

            return View(model);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _appointmentRepository.Get(id);
            if (appointment != null)
            {
                await _appointmentRepository.Delete(id);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
