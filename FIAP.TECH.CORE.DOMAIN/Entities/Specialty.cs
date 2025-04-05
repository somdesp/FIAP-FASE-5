namespace FIAP.TECH.CORE.DOMAIN.Entities
{
    public class Specialty : BaseEntity
    {
        public required string Name { get; set; }
        public List<Doctor> Doctor { get; set; }


    }
}
