using FIAP.TECH.CORE.DOMAIN.Entities;

namespace FIAP.TECH.CORE.DOMAIN.Models
{
    public class ErrosProcess
    {
        public bool Success { get; set; }
        public object Data { get; set; }
        public List<string> Errors { get; set; }
    }

    public record ScheduleResponse(bool Success, string Message, List<Schedule> Schedules);

    public record SpecialtyResponse(bool Success, string Message, List<Doctor> Doctors);
    public record SearchBySpecialty(string specialty);

}
