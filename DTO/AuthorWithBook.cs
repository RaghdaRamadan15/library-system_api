namespace day2.DTO
{
    public class AuthorWithBook
    {
        public int id { get; set; }
        public string Name { get; set; }
        public List<string>? bookTitle { get; set; }
    }
}
