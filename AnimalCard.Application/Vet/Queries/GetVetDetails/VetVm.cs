namespace AnimalCard.Application.Vet.Queries.GetVetDetails
{
    public class VetVm
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Surname { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string PhoneNumber { get; set; } = String.Empty;
        public string ProfilePicture { get; set; } = String.Empty;
        public string AboutMe { get; set; } = String.Empty;
        public List<AddressToShowDTO> Addresses { get; set; } = new List<AddressToShowDTO>();
        public List<DiseaseDTO> Diseases { get; set; } = new List<DiseaseDTO>();
        public List<ServiceTreatmentDTO> ServicesTreatments { get; set; } = new List<ServiceTreatmentDTO>(); 
    }
}