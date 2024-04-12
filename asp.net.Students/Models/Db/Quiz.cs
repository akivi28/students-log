namespace asp.net.Students.Models;

public class Quiz
{
    // public Quiz()
    // {
    //     Questions = new List<QuizQuestion>();
    // }
    public int Id { get; set; }
    public string Title { get; set; }
    public User Teacher { get; set; }
    public Subject? Subject { get; set; }
    public List<Group> Groups { get; set; } = new List<Group>();
    public IEnumerable<QuizQuestion> Questions { get; set; } = new List<QuizQuestion>();
}