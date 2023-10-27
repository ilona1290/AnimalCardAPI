using AnimalCard.Application.Pet.Queries.GetPetsByUserRole;

namespace AnimalCard.Application.Owner.Queries.GetOwner
{
    public class OwnerVm
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Surname { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string PhoneNumber { get; set; } = String.Empty;
        public string ProfilePicture { get; set; } = String.Empty;
        public UserPetsVm Pets { get; set; } = new UserPetsVm();
    }
}