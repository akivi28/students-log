namespace asp.net.Students.Areas.Teacher.Models;

public class QuestionFormData
{
    public int QuestionId { get; set; }
    public string QuestionText { get; set; }
    public int QuizId { get; set; }
    public List<string> OptionTexts { get; set; }
    public List<bool> IsCorrect { get; set; }
}