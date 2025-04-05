namespace FIAP.TECH.CORE.APPLICATION.DTO
{
    public class ScheduleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public required int IdDoctor { get; set; }
        public int? IdPatient { get; set; }
    }
}
