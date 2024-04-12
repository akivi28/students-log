using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using asp.net.Students.Areas.Teacher.Models;
using asp.net.Students.Models;
using Microsoft.EntityFrameworkCore;

namespace asp.net.Students.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    [Authorize(Roles = "Teacher, Admin")] 
    public class QuestionController : Controller
    {
        private readonly SiteContext _context;

        public QuestionController(SiteContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> AddEdit([FromBody] QuestionFormData formData)
        {
            QuizQuestion newQuestion = new QuizQuestion();
            List<QuizOption> options = new List<QuizOption>();
    
            for (int i = 0; i < formData.OptionTexts.Count; i++)
            {
                options.Add(new QuizOption(){Text = formData.OptionTexts[i], Value = formData.IsCorrect[i] ? PointsDistribution(formData.IsCorrect) : 0});
            }
            newQuestion.Text = formData.QuestionText;
            newQuestion.Options = options;

            var quiz = await _context.Quizzes.Include(quiz => quiz.Questions)
                .ThenInclude(quizQuestion => quizQuestion.Options).FirstOrDefaultAsync(q => q.Id == formData.QuizId);
            if (quiz != null)
            {
                if (formData.QuestionId != 0)
                {
                    var existingQuestion = quiz.Questions.FirstOrDefault(q => q.Id == formData.QuestionId);
                    if (existingQuestion != null)
                    {
                        existingQuestion.Text = newQuestion.Text;
                        _context.QuizOptions.RemoveRange(existingQuestion.Options);
                        existingQuestion.Options = existingQuestion.Options.ToList().Concat(options).ToList();
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    quiz.Questions = quiz.Questions.Append(newQuestion).ToList();
                    await _context.SaveChangesAsync();
                }
            }
            return Redirect("/Teacher/Quiz/Edit?id=" + formData.QuizId);
        }

        
        public float PointsDistribution(List<bool> list)
        {
            int right = list.Count(x => x == true);
            return (float)1 / right;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetQuestionById(int id)
        {
            var question = await _context.QuizQuestions
                .Include(q => q.Options)
                .FirstOrDefaultAsync(q => q.Id == id);
            if (question == null)
            {
                return NotFound();
            }

            var opt = new JsonSerializerOptions{
                ReferenceHandler = ReferenceHandler.Preserve
            };
            
            return Json(question,opt);
        }

        public async Task<IActionResult> Delete(int id, int quizId)
        {
            var question = await _context.QuizQuestions.FirstOrDefaultAsync(q => q.Id == id);
            _context.QuizQuestions.Remove(question);
            await _context.SaveChangesAsync();
            return Redirect("/Teacher/Quiz/Edit?id=" + quizId);
        }
    }
}