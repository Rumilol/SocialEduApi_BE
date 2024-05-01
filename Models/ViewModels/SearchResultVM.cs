﻿using Microsoft.EntityFrameworkCore;
using SocialEduApi.Data;
using SocialEduApi.Models.Entities;
using SocialEduApi.Models.Identity;

namespace SocialEduApi.Models.ViewModels
{
    public class SearchResultVM
    {
        public SearchResultVM() { }
        public SearchResultVM(int id, ApplicationUser user)
        {
            ID = id;
            Text = user.FirstName + " " + user.LastName;
            ItemID = user.Email;
            ItemType = "User";
            ItemImage = user.Image;
        }
        public SearchResultVM(int id, ProjectSubmission submission)
        {
            ID = id;
            Text = submission.Title + " - " + submission.User.FirstName + " " + submission.User.LastName;
            ItemID = submission.Id.ToString();
            ItemType = "ProjectSubmission";
            ItemImage = submission.ImageLink;
        }
        public SearchResultVM(int id, Institution institution)
        {
            ID = id;
            Text = institution.Name;
            ItemID = institution.Id.ToString();
            ItemType = "Institution";
            ItemImage = institution.Image;
        }
        public int? ID { get; set; }
        public string? Text { get; set; }
        public string? ItemID { get; set; }
        public string? ItemType { get; set; }
        public string? ItemImage { get; set; }

        public static List<SearchResultVM> GetGenericResults(ApplicationDbContext context, string email)
        {
            var results = new List<SearchResultVM>();
            var rnd = new Random();

            var users = context.Users.Where(p => p.Email != email).ToList();
            if (users.Any())
            {
                var user = users[rnd.Next(users.Count)];
                results.Add(new SearchResultVM(1, user));
            }

            var institutions = context.Institutions.ToList();
            if (institutions.Any())
            {
                var institution = institutions[rnd.Next(institutions.Count)];
                results.Add(new SearchResultVM(2, institution));
            }

            var submissions = context.ProjectSubmissions.Include(p => p.User).ToList();
            if (submissions.Any())
            {
                var submission = submissions[rnd.Next(submissions.Count)];
                results.Add(new SearchResultVM(3, submission));
            }
            return results;
        }
    }
}
