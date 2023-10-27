namespace AnimalCard.Application.Visit.Command.CompleteVisit
{
    public class TreatedDiseaseDTO
    {
        public string DiseaseName { get; set; } = String.Empty;
        public string DiseaseDescription { get; set;} = String.Empty;
        public string TreatmentDescription { get; set; } = String.Empty;
        public string PrescribedMedications { get; set; } = String.Empty;
        public string Recommendations { get; set; } = String.Empty;
    }
}