

using Bogus;
using Microsoft.AspNetCore.Identity;
using SocialEduApi.Data;
using SocialEduApi.Models.Identity;


namespace SocialEduApi.Models.Entities
{
    public class Institution
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Abbreviation { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Image { get; set; }
        public string? PageBackground { get; set; }


        public static Faker<Institution> GetFaker()
        {
            return new Faker<Institution>("hr")
                .RuleFor(x => x.Name, y => y.Company.CompanyName())
                .RuleFor(x => x.Abbreviation, y => y.Lorem.Letter(4))
                .RuleFor(x => x.Description, y => y.Lorem.Paragraph())
                //.RuleFor(x => x.Image, y => y.Image.LoremPixelUrl(LoremPixelCategory.Business))
                .RuleFor(x => x.Image, y => "")
                .RuleFor(x => x.PageBackground, y => "");
        }

    }
}
