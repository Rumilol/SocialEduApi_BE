using SocialEduApi.Data;
using SocialEduApi.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SocialEduApi.Models.ViewModels
{
    public class SavedUsersFolderVM
    {
        public SavedUsersFolderVM() { }
        public SavedUsersFolderVM(SavedUsersFolder folder, string email)
        {
            Id = folder.Id;
            UserEmail = email;
            Name = folder.Name;
            Users = folder.Users;
        }

        public int? Id { get; set; }
        public string? UserEmail { get; set; }
        public string Name { get; set; }

        [NotMapped]
        public List<UserVM_short>? Users { get; set; }
    }
}
