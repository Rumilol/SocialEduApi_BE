
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


    }
}
