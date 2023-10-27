namespace AnimalCard.Application.Pet.Queries.GetPetsByUserRole
{
    public class UserPetsVm 
    {
        public List<UserPetDTO> UserPets { get; set; } = new List<UserPetDTO>();
    }
}