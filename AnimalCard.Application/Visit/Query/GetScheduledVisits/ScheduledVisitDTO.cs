namespace AnimalCard.Application.Visit.Query.GetScheduledVisits
{
    public class ScheduledVisitDTO
    {
        public int Id { get; set; }
        public int VisitTypeId { get; set; }
        public string VisitTypeName { get; set; } = String.Empty;
        public int VetId { get; set;}
        public string Vet { get; set;} = String.Empty;
        public int OwnerId { get; set; }
        public string Owner { get; set; } = String.Empty;
        public int PatientId { get; set;}
        public string Patient { get; set; } = String.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ExtraInfo { get; set; } = String.Empty;
        public bool IsCompleted { get; set; }
    }
}