using InfrastructureLayer.Entities.Base;

namespace InfrastructureLayer.Entities
{
    public class AliasNames : Entity<int>
    {
        public string Name { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }
    }
}
