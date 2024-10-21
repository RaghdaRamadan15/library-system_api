namespace day2.DTO
{
    public class labwithauthor
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public List<int>? Author_id{ get; set; }
        public List<string>? Auhtors { get; set; }
    }
}
