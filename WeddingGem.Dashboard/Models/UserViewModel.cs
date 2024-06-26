﻿using Microsoft.AspNetCore.Identity;

namespace WeddingGem.Dashboard.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? packName { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
