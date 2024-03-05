﻿namespace EducationlPlatform.Models.Context
{
    public class IdentityUserDbContext : IdentityDbContext<User,IdentityRole,string>
    {
        public IdentityUserDbContext(DbContextOptions<IdentityUserDbContext> option) : base(option)
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}
