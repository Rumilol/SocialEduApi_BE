
using Bogus;
using Microsoft.EntityFrameworkCore;
using SocialEduApi.Data;
using SocialEduApi.Models.Identity;

using System.ComponentModel.DataAnnotations.Schema;

namespace SocialEduApi.Models.Entities
{
    public class SavedUsersFolder
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public string Name { get; set; }

        public static Faker<SavedUsersFolder> GetFaker(string userID)
        {
            return new Faker<SavedUsersFolder>("hr")
                .RuleFor(x => x.UserID, y => userID)
                .RuleFor(x => x.Name, y => y.Commerce.Department() + " " + y.Hacker.IngVerb());
        }
    }
}
