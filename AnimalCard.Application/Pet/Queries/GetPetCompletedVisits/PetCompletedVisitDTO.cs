namespace AnimalCard.Application.Pet.Queries.GetPetCompletedVisits
{
    public class PetCompletedVisitDTO
    {
        public int Id { get; set; }
        public string VisitCardFileName { get; set; } = String.Empty;
        public string VisitCardPath { get; set; } = String.Empty;
        public DateOnly VisitDate { get; set; }
        public string Vet { get; set; } = String.Empty;
    }
}