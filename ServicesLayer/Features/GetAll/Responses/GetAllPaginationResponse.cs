namespace ServicesLayer.Features.GetAll.Responses
{
    public record GetAllPaginationResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Degree { get; set; }
        public List<AliasName> AliasNames { get; set; }

    };

    public class AliasName
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }


}
