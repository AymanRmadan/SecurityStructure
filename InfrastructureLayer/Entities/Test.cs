using InfrastructureLayer.Entities.Base;
using InfrastructureLayer.Enums.EnumsClasses;

namespace InfrastructureLayer.Entities
{
    public class Test : Entity<int>
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public Degree Degree { get; set; }
        public List<AliasNames> AliasNames { get; set; }
    }
}
