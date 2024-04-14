namespace SocialEduApi.Models.Entities
{
    public class InstitutionRole
    {
        public enum Roles
        {
            Student = 1,
            Professor = 2,
            Admin = 3,
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
