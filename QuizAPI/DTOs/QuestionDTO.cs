namespace QuizAPI.DTOs
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string Answer4 { get; set; }
    }
    public class GetQuestionDTO : QuestionDTO
    {
        public byte[] Image { get; set; }
    }
    public class SaveQuestionDTO : QuestionDTO
    {
        public string CorrectAnswer { get; set; }
        public IFormFile Image { get; set; }
    }
    public class SubmitAnswerDTO
    {
        public int QuestionId { get; set; }
        public string Answer { get; set; }
        public string Username { get; set; }
    }
    public class APIResult<T> where T : class
    {
        public APIResult()
        {
            Status = true;
            TotalRecords = 0;
        }
        public T? Data { get; set; }
        public string? Message { get; set; }
        public bool Status { get; set; }
        public int TotalRecords { get; set; }
    }
}
