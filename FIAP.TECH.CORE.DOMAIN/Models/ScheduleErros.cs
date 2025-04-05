using FIAP.TECH.CORE.DOMAIN.Entities;

namespace FIAP.TECH.CORE.DOMAIN.Models
{
    public class ScheduleErros
    {
        public bool Success { get; set; }
        public Schedule Data { get; set; }
        public List<string> Errors { get; set; }
    }

    public record ScheduleResponse(bool Success, string Message, List<Schedule> Contacts);
    public record ContactByDDD(string DDD);

}
