using System.Collections;

namespace asp.net.Students.Models;

public class QuizQuestion : IEnumerable
{
    public int Id { get; set; }
    public string Text { get; set; }
    public Quiz Quiz { get; set; }
    public IEnumerable<QuizOption> Options { get; set; } = new List<QuizOption>();
    public IEnumerator GetEnumerator()
    {
        throw new NotImplementedException();
    }
}