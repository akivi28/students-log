namespace asp.net.Students.Models;

public class Subject
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Info>? StudentsList { get; set; }
}