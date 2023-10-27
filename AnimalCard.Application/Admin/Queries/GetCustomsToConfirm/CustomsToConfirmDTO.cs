namespace AnimalCard.Application.Admin.Queries.GetCustomsToConfirm
{
    public class CustomsToConfirmDTO
    {
        public int IdVet { get; set; }
        public string VetName { get; set; } = String.Empty;
        public string VetSurname { get; set; } = String.Empty;
        public List<CustomDiseaseToConfirmDTO> DiseasesToConfirm { get; set; } = new List<CustomDiseaseToConfirmDTO>();
        public List<CustomServiceTreatmentToConfirmDTO> ServicesTreatmentsToConfirm { get; set; } = new List<CustomServiceTreatmentToConfirmDTO>();
    }
}