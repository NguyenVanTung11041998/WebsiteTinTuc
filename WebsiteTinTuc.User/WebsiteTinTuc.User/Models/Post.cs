﻿using System;

namespace WebsiteTinTuc.User.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string PostUrl { get; set; }
        public string AgencyName { get; set; }
        public bool IsCreate { get; set; }
    }
}
