﻿namespace EducationlPlatform.Models.Users
{
    public class Tutor
    {
        public int Id { get; set; }

        public string Description { get; set; }
        
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        
        public virtual ICollection<StudentTutorRoom> StudentTutorRooms { get; set; }


        public ICollection<Contact> Contacts { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
