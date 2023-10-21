﻿using Microsoft.AspNetCore.Identity;

namespace EmlakOfisiSitesi.Models.Entities
{
    public class Admin : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
