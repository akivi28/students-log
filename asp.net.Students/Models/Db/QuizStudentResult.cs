namespace asp.net.Students.Models;

public class QuizStudentResult
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public User Student { get; set; }
    public Quiz Quiz { get; set; }
    public float Score { get; set; }
    public List<QuizStudentAnswer> StudentAnswers { get; set; } = new List<QuizStudentAnswer>();
}