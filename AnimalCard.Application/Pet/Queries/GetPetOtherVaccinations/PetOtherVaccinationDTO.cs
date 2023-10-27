namespace AnimalCard.Application.Pet.Queries.GetPetOtherVaccinations
{
    public class PetOtherVaccinationDTO
    {
        public int Id { get; set; }
        public string DiseaseName { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty;
        public string Series { get; set; } = String.Empty;
        public DateOnly VaccinationDate { get; set; }
        public string Vet { get; set; } = String.Empty;
    }
}