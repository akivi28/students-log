namespace asp.net.Students.Models;

public class QuizOption
{
    public int Id { get; set; }
    public string Text { get; set; }
    public float Value { get; set; }
    public QuizQuestion Question { get; set; }
}