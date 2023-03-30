using System;
using System.Collections.Generic;

namespace QuizAPI.Models
{
    public partial class Content
    {
        public int QuestionId { get; set; }
        public string Question { get; set; } = null!;
        public string Answer1 { get; set; } = null!;
        public string Answer2 { get; set; } = null!;
        public string Answer3 { get; set; } = null!;
        public string Answer4 { get; set; } = null!;
        public string CorrectAnswer { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string? Username { get; set; }

        public virtual UserContent? UsernameNavigation { get; set; }
    }
}
