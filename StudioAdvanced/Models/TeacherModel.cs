using System;

namespace StudioAdvanced.Models
{
    public class TeacherModel
    {
        public int TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Postal { get; set; }
        public string PrimaryPhone { get; set; }
        public string Email { get; set; }
        public string SecondaryPhone { get; set; }
        public decimal PayRate { get; set; }
        public int Pin { get; set; }
        public DateTime Birthday { get; set; }

    }
}
