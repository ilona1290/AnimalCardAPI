namespace AnimalCard.Application.Pet.Queries.GetPetRabiesVaccinations
{
    public class PetRabiesVaccinationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Series { get; set; } = String.Empty;
        public DateOnly VaccinationDate { get; set; }
        public DateOnly TermValidity { get; set; }
        public DateOnly TermNext { get; set; }
        public string Vet { get; set; } = String.Empty;
    }
}