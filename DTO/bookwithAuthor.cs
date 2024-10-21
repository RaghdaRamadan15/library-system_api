namespace day2.DTO
{
    public class bookwithAuthor
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public int PageCount { get; set; }

        public decimal Price { get; set; }
        public string Publisher { get; set; }
        public int? AuthorId { get; set; }
        public string? AuthersName { get; set; }
    }
}
