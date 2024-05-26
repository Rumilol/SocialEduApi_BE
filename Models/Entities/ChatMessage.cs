using SocialEduApi.Models.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialEduApi.Models.Entities
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string SenderID { get; set; }
        public ApplicationUser Sender { get; set; }
        public string RecipientID { get; set; }
        public ApplicationUser Recipient { get; set; }
        public string Content { get; set; }
        public DateTime TimeStamp { get; set; }
        
        [NotMapped]
        public string UniqueChatId
        {
            get
            {
                var ids = new List<string>() { SenderID, RecipientID };
                ids = ids.Order().ToList();
                return string.Join(";;", ids);
            }
        }
    }
}
