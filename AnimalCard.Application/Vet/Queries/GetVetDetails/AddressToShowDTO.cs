namespace AnimalCard.Application.Vet.Queries.GetVetDetails
{
    public class AddressToShowDTO
    {
        public int Id { get; set; }
        public string? NameOfPlace { get; set; } = String.Empty;
        public string City { get; set; } = String.Empty;
        public string? District { get; set; } = String.Empty;
        public string Street { get; set; } = String.Empty;
        public int HouseNumber { get; set; }
        public int? PremisesNumber { get; set; }
    }
}