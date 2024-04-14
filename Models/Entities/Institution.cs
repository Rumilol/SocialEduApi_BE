﻿

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




    }
}