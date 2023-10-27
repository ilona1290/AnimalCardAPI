namespace AnimalCard.Application.Pet.Queries.GetPetTreatments
{
    public class PetTreatmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Diagnosis { get; set; } = String.Empty;
        public string TreatmentDescription { get; set; } = String.Empty;
        public string Recommendations { get; set; } = String.Empty;
        public DateOnly TreatmentDate { get; set;}
        public string Vet { get; set; } = String.Empty;
    }
}