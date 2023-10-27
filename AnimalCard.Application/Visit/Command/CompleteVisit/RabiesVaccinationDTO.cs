namespace AnimalCard.Application.Visit.Command.CompleteVisit
{
    public class RabiesVaccinationDTO
    {
        public string Name { get; set; } = String.Empty;
        public string Series { get; set; } = String.Empty;
        public DateTime TermValidityRabies { get; set; }
        public DateTime TermNextRabies { get; set; }
    }
}