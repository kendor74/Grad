﻿namespace EducationlPlatform.Models.User
{
    public class Admin : User
    {
        public List<string> Comments { get; set; }
        public virtual User User { get; set; }
    }
}
