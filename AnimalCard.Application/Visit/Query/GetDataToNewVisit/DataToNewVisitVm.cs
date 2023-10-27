using AnimalCard.Application.Owner.Queries.GetOwners;
using AnimalCard.Application.Pet.Queries.GetPetsByUserRole;

namespace AnimalCard.Application.Visit.Query.GetDataToNewVisit
{
    public class DataToNewVisitVm
    {
		public List<VisitTypeDTO> VisitTypes { get; set; } = new List<VisitTypeDTO>();
		public List<OwnerWithPetsDTO> Owners { get; set; } = new List<OwnerWithPetsDTO>();
		public List<DisabledTermDTO> DisabledTerms { get; set; } = new List<DisabledTermDTO>();
	}
}