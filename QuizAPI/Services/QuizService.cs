using Microsoft.EntityFrameworkCore;
using QuizAPI.DTOs;
using QuizAPI.Models;

namespace QuizAPI.Services
{
    public class QuizService
    {
        private readonly QuizDbContext _dbContext;
        public QuizService(QuizDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public APIResult<List<UserContent>> GetReport()
        {
            try
            {
                var usersByWins = _dbContext.UserContents.OrderByDescending(u => u.Wins).ToList();
                return new APIResult<List<UserContent>> { Data = usersByWins, TotalRecords = usersByWins.Count };
            }
            catch (Exception ex)
            {
                return new APIResult<List<UserContent>> { Status = false, Message = ex.Message };
            }
        }
        public APIResult<object> SubmitAnswer(SubmitAnswerDTO obj)
        {
            try
            {
                int answerFlag = 0;
                //Check whether the question and user exists or not
                var question = _dbContext.Contents.Find(obj.QuestionId);
                var user = _dbContext.UserContents.Find(obj.Username);
                if (question is null || user is null)
                {
                    throw new Exception("Invalid Opreation");
                }

                //Validate Answer
                if (question.CorrectAnswer.ToLower().Trim() == obj.Answer.ToLower().Trim())
                {
                    //Check if the current user answered first
                    if (String.IsNullOrEmpty(question.Username))
                        question.Username = obj.Username;

                    user.Wins++;
                    answerFlag = 1;
                }
                else
                    user.Losses++;

                _dbContext.SaveChanges();

                return new APIResult<object> { Data = (answerFlag == 0) ? "Wrong Answer" : "Correct Answer" };
            }
            catch (Exception ex)
            {
                return new APIResult<object> { Status = false, Message = ex.Message };
            }
        }
    }
}
