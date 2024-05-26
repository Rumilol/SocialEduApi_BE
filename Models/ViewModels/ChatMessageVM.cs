using SocialEduApi.Models.Entities;
using SocialEduApi.Models.Identity;

namespace SocialEduApi.Models.ViewModels
{
    public class ChatMessageVM
    {
        public ChatMessageVM() { }
        public ChatMessageVM(ChatMessage chat)
        {
            Id = chat.Id;
            SenderEmail = chat.Sender.Email;
            RecipientEmail = chat.Recipient.Email;
            Content = chat.Content;
            TimeStamp = chat.TimeStamp;
            UniqueChatId = chat.UniqueChatId;
        }
        public int? Id { get; set; }
        public string? SenderEmail { get; set; }
        public string? RecipientEmail { get; set; }
        public string? Content { get; set; }
        public DateTime? TimeStamp { get; set; }
        public string? UniqueChatId { get; set; }
    }
}
