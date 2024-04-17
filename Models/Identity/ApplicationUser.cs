
using Bogus;
using Microsoft.AspNetCore.Identity;

namespace SocialEduApi.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender {  get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string UserpageBackground { get; set; } = string.Empty;
        public string General { get; set; } = string.Empty;
        public string Interests { get; set; } = string.Empty;
        public string Education { get; set; } = string.Empty;
        public string Experience { get; set; } = string.Empty;
        public string Links { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public DateTime JoinedDate { get; set; }

        public static Faker<ApplicationUser> GetFaker()
        {
            return new Faker<ApplicationUser>("hr")
                .RuleFor(x => x.UserName, y => y.Person.Email)
                .RuleFor(x => x.Email, y => y.Person.Email)
                .RuleFor(x => x.EmailConfirmed, y => true)
                .RuleFor(x => x.LockoutEnabled, y => false)
                .RuleFor(x => x.FirstName, y => y.Person.FirstName)
                .RuleFor(x => x.LastName, y => y.Person.LastName)
                .RuleFor(x => x.Gender, y => y.Random.Bool() ? "M" : "Ž")
                .RuleFor(x => x.Location, y => y.Address.City())
                .RuleFor(x => x.Description, y => y.Lorem.Paragraph())
                .RuleFor(x => x.Image, y => "")
                .RuleFor(x => x.UserpageBackground, y => "")
                .RuleFor(x => x.DateOfBirth, y => y.Person.DateOfBirth)
                .RuleFor(x => x.JoinedDate, y => y.Date.Between(DateTime.Today.AddYears(-10), DateTime.Today))
                .RuleFor(x => x.General, y => $"{y.Hacker.Noun()};;{y.Hacker.Noun()};;{y.Hacker.Noun()}")
                .RuleFor(x => x.Interests, y => $"{y.Hacker.IngVerb()};;{y.Hacker.Noun()};;{y.Hacker.IngVerb()}")
                .RuleFor(x => x.Education, y => "Srednja škola;;Preddiplomski studij;;Diplomski studij")
                .RuleFor(x => x.Experience, y => "2014 - 2020: Sveučilište 1;;2020 - .: Kompanija 1")
                .RuleFor(x => x.Links, y => "https://github.com/;;https://www.facebook.com/;;https://hr.linkedin.com/");
        }
    }
}
