namespace BlogProject.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public int BlogID { get; set; }
        public DateTime PublishDate { get; set; }
        public string Author { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
