namespace InfrastructureLayer.Entities.Base
{
    public abstract class Entity<Key> where Key : struct
    {
        public Key Id { get; set; }
    }
}
