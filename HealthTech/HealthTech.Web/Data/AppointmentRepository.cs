using Microsoft.EntityFrameworkCore;

namespace HealthTech.Web.Data
{
    public interface IAppointmentRepository
    {
        Task Create(Appointment appointment);
        Task Delete(int id);
        Task<Appointment> Get(int id);
        Task<List<Appointment>> GetMany();
        Task<Appointment> Update(Appointment appointment);
    }

    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AppointmentRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
        }
        public async Task<Appointment> Get(int id)
        {
            return await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<Appointment>> GetMany()
        {
            return await _context.Appointments
                .OrderByDescending(a => a.Date)
                .ToListAsync();
        }

        public async Task<Appointment> Update(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task Delete(int id)
        {
            var appointment = await Get(id);
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
        }
    }
}
