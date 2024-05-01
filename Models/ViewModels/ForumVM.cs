using SocialEduApi.Models.Entities;

namespace SocialEduApi.Models.ViewModels
{
    public class ForumVM
    {
        public ForumVM() { }
        public ForumVM(Forum forum) 
        {
            Id = forum.Id;
            SubjectID = forum.SubjectID;
            Subject = forum.Subject;
            Name = forum.Name;
            Description = forum.Description;
            CreatedDate = forum.CreatedDate;
        }

        public ForumVM(Forum forum, List<ForumPost>? posts) : this(forum)
        {
            GetForumPosts(posts);
        }

        public int? Id { get; set; }
        public int? SubjectID { get; set; }
        public Subject? Subject { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; } = string.Empty;
        public DateTime? CreatedDate { get; set; }

        public List<ForumPostVM>? ForumPosts { get; set; }

        private void GetForumPosts(List<ForumPost>? posts)
        {
            if (posts == null) return;
            ForumPosts = posts.Where(p => p.ForumID == Id)
                .Select(p => new ForumPostVM(p))
                .ToList();
        }
        public Forum ToForum()
        {
            var forum = new Forum();
            forum.Id = Id.GetValueOrDefault();
            forum.SubjectID = SubjectID.GetValueOrDefault();
            forum.Name = Name;
            forum.Description = Description;
            forum.CreatedDate = CreatedDate.GetValueOrDefault();
            return forum;
        }
    }
}
