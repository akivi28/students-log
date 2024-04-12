namespace asp.net.Students.Models;

public class QuizStudentAnswer
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public User Student { get; set; }
    public int QuizOptionId { get; set; }
    public QuizOption Option { get; set; }
}