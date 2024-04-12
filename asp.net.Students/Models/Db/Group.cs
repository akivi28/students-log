namespace asp.net.Students.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Info>? Members { get; set; }
        public IEnumerable<Quiz> Quizzes { get; set; }
    }
}