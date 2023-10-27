using AnimalCard.Application.Vet.Queries.GetVetDetails;

namespace AnimalCard.Application.Vet.Queries.GetVetDetailsToEdit
{
    public class VetToEditVm
    {
        public string ProfilePicture { get; set; } = String.Empty;
        public string AboutMe { get; set; } = String.Empty;
        public List<AddressToShowDTO> Addresses { get; set; } = new List<AddressToShowDTO>();
        public List<DiseaseDTO> Diseases { get; set; } = new List<DiseaseDTO>();
        public List<DiseaseDTO> CustomDiseases { get; set; } = new List<DiseaseDTO>();
        public List<ServiceTreatmentDTO> ServicesTreatments { get; set; } = new List<ServiceTreatmentDTO>();
        public List<ServiceTreatmentDTO> CustomServicesTreatments { get; set; } = new List<ServiceTreatmentDTO>();
    }
}