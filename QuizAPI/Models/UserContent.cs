using System;
using System.Collections.Generic;

namespace QuizAPI.Models
{
    public partial class UserContent
    {
        public UserContent()
        {
            Contents = new HashSet<Content>();
        }

        public string Username { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? DigitNumber { get; set; }
        public int? Wins { get; set; }
        public int? Losses { get; set; }

        public virtual ICollection<Content> Contents { get; set; }
    }
}
