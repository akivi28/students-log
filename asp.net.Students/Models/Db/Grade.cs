namespace asp.net.Students.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public int InfoId { get; set; }
        public Info Info { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public int Value { get; set; }
    }
}
