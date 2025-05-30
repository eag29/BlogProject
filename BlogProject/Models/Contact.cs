namespace BlogProject.Models
{
    public class Contact
    {
        public int ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
