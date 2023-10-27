namespace AnimalCard.Application.Pet.Queries.GetPet
{
    public class PetVm
    {
        public int Id { get; set; }
        public string IdentityNumber { get; set; } = String.Empty;
        public string Photo { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty;
        public string Sex { get; set; } = String.Empty;
        public DateOnly DateBirth { get; set; }
        public string Breed { get; set; } = String.Empty;
        public string Color { get; set; } = String.Empty;
        public string HairType { get; set; } = String.Empty;
        public string Trademarks { get; set; } = String.Empty;
        public string Allergies { get; set; } = String.Empty;
        public string ExtraInfo { get; set; } = String.Empty;
        public string Owner { get; set; } = String.Empty;
    }
}