
using Bogus;
using Microsoft.EntityFrameworkCore;
using SocialEduApi.Data;
using SocialEduApi.Models.Identity;
using SocialEduApi.Models.ViewModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialEduApi.Models.Entities
{
    public class SavedUsersFolder
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public string Name { get; set; }

        [NotMapped]
        public List<UserVM_short> Users { get; set; }

        public void GetUsers(ApplicationDbContext context)
        {
            Users = new List<UserVM_short>();
            var savedUsers = context.SavedUsers.Include(p => p.User).Where(p => p.FolderID == Id).ToList();
            foreach (var a in savedUsers)
            {
                var vm = new UserVM_short(a.User);
                Users.Add(vm);
            }
        }

        public static Faker<SavedUsersFolder> GetFaker(string userID)
        {
            return new Faker<SavedUsersFolder>("hr")
                .RuleFor(x => x.UserID, y => userID)
                .RuleFor(x => x.Name, y => y.Commerce.Department() + " " + y.Hacker.IngVerb());
        }
    }
}
