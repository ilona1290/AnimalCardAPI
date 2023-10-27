namespace AnimalCard.Application.Pet.Queries.GetPetTreatedDiseases
{
    public class PetTreatedDiseaseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string DiseaseDescription { get; set; } = String.Empty;
        public string TreatmentDescription { get; set; } = String.Empty;
        public string PrescribedMedications { get; set; } = String.Empty;
        public string Recommendations { get; set; } = String.Empty;
        public DateOnly DiagnosisDate { get; set; }
        public string Vet { get; set; } = String.Empty;
    }
}