
using Bogus;
using SocialEduApi.Models.Identity;

namespace SocialEduApi.Models.Entities
{
    public class UserInstitutionBelongs
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public ApplicationUser? User { get; set; }
        public int InstitutionID { get; set; }
        public Institution? Institution { get; set; }
        public int RoleID { get; set; }
        public InstitutionRole? Role { get; set; }

        public static Faker<UserInstitutionBelongs> GetFaker(List<string> userIDs, int institutionID)
        {
            return new Faker<UserInstitutionBelongs>("hr")
                .RuleFor(x => x.UserID, y => y.PickRandom(userIDs))
                .RuleFor(x => x.InstitutionID, y => institutionID)
                .RuleFor(x => x.RoleID, y => y.PickRandom(1, 2));
        }
    }
}
