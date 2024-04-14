namespace SocialEduApi.Models.Entities
{
    public class Notification
    {
        public string UserID { get; set; }
        public DateTime Timestamp { get; set; }
        public string Message { get; set; }
        public bool Opened { get; set; }
    }
}
