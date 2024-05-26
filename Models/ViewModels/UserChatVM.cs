using SocialEduApi.Models.Entities;
using SocialEduApi.Models.Identity;

namespace SocialEduApi.Models.ViewModels
{
    public class UserChatVM
    {
        public UserChatVM() { }
        public UserChatVM(ApplicationUser user, List<ChatMessage> messages)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Image = user.Image;
            Messages = messages.Select(p => new ChatMessageVM(p))
                               .OrderByDescending(p => p.TimeStamp)
                               .ToList();
        }
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Image { get; set; } = string.Empty;
        public List<ChatMessageVM>? Messages { get; set; }
    }
}
