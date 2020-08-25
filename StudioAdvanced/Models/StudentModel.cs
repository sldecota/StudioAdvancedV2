using System;

namespace StudioAdvanced.Models
{
    public class StudentModel
    {
        public int StudentId { get; set; }
        public int FamilyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public bool Competition { get; set; }
        public bool IsAssistant { get; set; }
        public bool MBDiscount { get; set; }
        public string Notes { get; set; }
        public int LoginPin { get; set; }
        public int AssistantCredit { get; set; }

    }
}
