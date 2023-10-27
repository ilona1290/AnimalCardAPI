namespace AnimalCard.Application.Vet.Queries.GetVets
{
    public class VetDTO
    {
        public int Id { get; set; }
        public string Vet { get; set; } = String.Empty;
        public string ProfilePicture { get; set; } = String.Empty;
        public string VetsCitiesDistricts { get; set; } = String.Empty;
        public string VetDiseases { get; set; } = String.Empty;
        public string VetServicesTreatments { get; set; } = String.Empty;
    }
}