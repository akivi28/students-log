namespace asp.net.Students.Models
{
    public class Info
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDay { get; set; }
        public string Description { get; set; }
        public bool IsFree { get; set; }
        public List<Photo>? Photos { get; set; }
        public int? GroupId { get; set; }
        public Group? Group { get; set; }
        public List<Subject>? Subjects { get; set; }
    }

    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int InfoId { get; set; }
        public Info Info { get; set; }
    }
}