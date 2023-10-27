namespace AnimalCard.Application.Pet.Queries.GetPetResearches
{
    public class PetReaserchDTO
    {
        public int Id { get; set; }
        public string ResearchesList { get; set; } = String.Empty;
        public DateOnly ResearchesDate { get; set; }
        public string ResultFileName { get; set; } = String.Empty;
        public string ResultPath { get; set; } = String.Empty;
        public DateOnly? ResultDate { get; set; }
        public string Vet { get; set; } = String.Empty;
    }
}