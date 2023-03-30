using QuizAPI.DTOs;
using QuizAPI.Models;
using System.Text;

namespace QuizAPI.Services
{
    public class QuestionsService
    {
        private readonly QuizDbContext _dbContext;
        public QuestionsService(QuizDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public APIResult<List<GetQuestionDTO>> Get()
        {
            try
            {
                var data = (from questions in _dbContext.Contents
                            select new GetQuestionDTO()
                            {
                                Answer1 = questions.Answer1,
                                Answer2 = questions.Answer2,
                                Answer3 = questions.Answer3,
                                Answer4 = questions.Answer4,
                                Id = questions.QuestionId,
                                Image = Encoding.UTF8.GetBytes(questions.Image),
                                Question = questions.Question,
                            }).ToList();

                var result = new APIResult<List<GetQuestionDTO>>
                {
                    Data = data,
                    TotalRecords = data.Count,
                };
                return result;
            }
            catch (Exception ex)
            {
                return new APIResult<List<GetQuestionDTO>> { Status = false, Message = ex.Message };
            }
        }
        public APIResult<GetQuestionDTO> Get(int id)
        {
            try
            {
                var data = (from questions in _dbContext.Contents
                            where questions.QuestionId == id
                            select new GetQuestionDTO()
                            {
                                Answer1 = questions.Answer1,
                                Answer2 = questions.Answer2,
                                Answer3 = questions.Answer3,
                                Answer4 = questions.Answer4,
                                Id = questions.QuestionId,
                                Image = Encoding.UTF8.GetBytes(questions.Image),
                                Question = questions.Question,
                            }).FirstOrDefault();

                var result = new APIResult<GetQuestionDTO>
                {
                    Data = data,
                };
                return result;
            }
            catch (Exception ex)
            {
                return new APIResult<GetQuestionDTO> { Status = false, Message = ex.Message };
            }
        }
        public async Task<APIResult<object>> SaveAsync(SaveQuestionDTO questionDTO)
        {
            try
            {
                byte[] imageData = null;

                if (questionDTO.Image != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        await questionDTO.Image.CopyToAsync(ms);
                        imageData = ms.ToArray();
                    }
                }

                var entity = new Content
                {
                    Answer1 = questionDTO.Answer1,
                    Answer2 = questionDTO.Answer2,
                    Answer3 = questionDTO.Answer3,
                    Answer4 = questionDTO.Answer4,
                    CorrectAnswer = questionDTO.CorrectAnswer,
                    Image = (imageData is null) ? String.Empty : Convert.ToBase64String(imageData),
                    Question = questionDTO.Question,
                    QuestionId = questionDTO.Id
                };

                if (entity.QuestionId > 0)
                    _dbContext.Contents.Update(entity);
                else
                    _dbContext.Contents.Add(entity);

                _dbContext.SaveChanges();

                return new APIResult<object> { Message = "Saved" };
            }
            catch (Exception ex)
            {
                return new APIResult<object> { Status = false, Message = ex.Message };
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
